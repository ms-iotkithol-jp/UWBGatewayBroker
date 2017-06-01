using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    public interface IGatewayModule
    {
        void Create(Broker broker, byte[] configuration);
        void Destroy();
        void Receive(Message received_message);
    }
}
