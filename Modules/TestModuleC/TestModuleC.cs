using EGEdge.IoT.Gateway;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModuleC
{
    public class TestModuleC : IGatewayModule
    {
        private string name = "TestModuleC";
        Broker myBroker;
        public void Create(Broker broker, byte[] configuration)
        {
            myBroker = broker;
            Debug.WriteLine("{0}:Create called.", name);
            Debug.WriteLine(" config:{0}", Encoding.UTF8.GetString(configuration));

        }

        public void Start()
        {
            Debug.WriteLine("{0}:Start called.", name);
            new Task(() =>
            {
                string content = "Hello@" + DateTime.Now.ToString();
                var message = new Message(Encoding.UTF8.GetBytes(content));
                message.Properties.Add("module", name);
                Task.Delay(TimeSpan.FromSeconds(30));
                myBroker.Publish(message);
            }).Start();
        }

        public void Destroy()
        {
            Debug.WriteLine("{0}:Destory called.", name);
        }

        public void Receive(Message received_message)
        {
            Debug.WriteLine("[0]:Receive Called", name);
            Debug.WriteLine(" Properties:");
            foreach (var key in received_message.Properties.Keys)
            {
                Debug.WriteLine("  {0}:{1}", key, received_message.Properties[key]);
            }
            Debug.WriteLine("  Meessage:{0}", Encoding.UTF8.GetString(received_message.Content));
        }
    }
}
