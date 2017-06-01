using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    public class Gateway
    {
        public Gateway()
        {
            messageBus = new MessageBus();
            messageBus.SetRegistedModules(gwModules);
        }

        private MessageBus messageBus;
        private Dictionary<string, IGatewayModule> gwModules = new Dictionary<string, IGatewayModule>();
        private int moduleIndex = 0;
        public void AddInitialModule(string name, IGatewayModule module)
        {
            lock (gwModules)
            {
                gwModules.Add(name, module);
            }
        }

        public void CreateFromJson(string json)
        {
            dynamic root = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            var classInstance = Type.GetType("TestClass");

            var modules = root.SelectToken("modules");
            foreach (var module in modules)
            {
                string moduleConfig = ((JObject)module).ToString();
                dynamic nameToken = module.SelectToken("name");
                string name = nameToken.Value;
                dynamic loaderToken = module.SelectToken("loader");
                string modulePath = "";
                dynamic epToken = loaderToken.SelectToken("entrypoint");
                foreach (var c in ((JObject)epToken).Children())
                {
                    modulePath = c.ToString();
                    if (modulePath.IndexOf("module.path") > 0)
                    {
                        modulePath = modulePath.Substring(modulePath.IndexOf(":") + 1);
                        modulePath = modulePath.Substring(2, modulePath.Length - 3);
                        string currentPath = System.IO.Directory.GetCurrentDirectory();
                        break;
                    }
                }
                var broker = new UWPBroker(messageBus, ++moduleIndex, moduleIndex);
                broker.SetTargetModule(name,gwModules[name]);
                dynamic argsToken = module.SelectToken("args");
                gwModules[name].Create(broker, Encoding.UTF8.GetBytes(argsToken.ToString()));

            }
            var links = root.SelectToken("links");
            foreach(var link in links)
            {
                dynamic sourceToken = link.SelectToken("source");
                dynamic sinkToken = link.SelectToken("sink");
                string source = sourceToken.Value;
                string sink = sinkToken.Value;
                messageBus.AddLink(source, sink);
            }
        }

        public void Destroy()
        {
            lock (gwModules)
            {
                foreach(var name in gwModules.Keys)
                {
                    gwModules[name].Destroy();
                }
            }
        }

    }
}