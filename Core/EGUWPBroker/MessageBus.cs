using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    class MessageBus
    {
        Dictionary<string, IGatewayModule> registedModules;
        Dictionary<string, List<string>> linkedModules = new Dictionary<string, List<string>>();

        public void SetRegistedModules(Dictionary<string,IGatewayModule> rm)
        {
            registedModules = rm;
        }
        public void AddLink(string sender, string receiver)
        {
            lock (linkedModules)
            {
                if (!linkedModules.ContainsKey(sender))
                {
                    linkedModules.Add(sender, new List<string>());
                }
                linkedModules[sender].Add(receiver);
            }
        }


        public void SendMessage(string sender, Message msg)
        {
            new Task(() =>
            {
                var receivers = new List<IGatewayModule>();
                lock (linkedModules)
                {
                    foreach (var key in linkedModules.Keys)
                    {
                        if (key == sender)
                        {
                            foreach (var m in linkedModules[key])
                            {
                                receivers.Add(registedModules[m]);
                            }
                        }
                    }
                }
                if (receivers.Count == 0)
                {
                    lock (registedModules)
                    {
                        foreach (var name in registedModules.Keys)
                        {
                            if (sender != name)
                            {
                                receivers.Add(registedModules[name]);
                            }
                        }
                    }
                }
                foreach (var r in receivers)
                {
                    new Task(() =>
                    {
                        var rMsg = new Message(msg);
                        r.Receive(rMsg);
                    }).Start();
                }
            }
            ).Start();
        }
    }
}
