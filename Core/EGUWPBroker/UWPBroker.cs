using EGEdge.IoT.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    class UWPBroker : Broker
    {
        string targetName;
        IGatewayModule targetModule;
        MessageBus messageBus;

        public UWPBroker(MessageBus mbus, long b, long m) :base(b,m)
        {
            messageBus = mbus;
        }

        public void SetTargetModule(string name, IGatewayModule module)
        {
            targetModule = module;
            targetName = name;
        }

        public override void Publish(Message message)
        {
            messageBus.SendMessage(targetName, message);
        }
    }
}
