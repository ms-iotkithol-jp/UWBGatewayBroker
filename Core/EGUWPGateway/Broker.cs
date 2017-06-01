using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    public abstract class Broker
    {
        public Broker(long broker, long module) { }
        public Broker(long broker, long module, NativeDotNetHostWrapper nativeWrapper) {
            throw new NotImplementedException();
        }

        abstract public void Publish(Message message);
    }
}
