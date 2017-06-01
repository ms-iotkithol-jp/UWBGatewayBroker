using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    public abstract class NativeDotNetHostWrapper
    {
        public NativeDotNetHostWrapper() { }

        public static bool Module_DotNetHost_PublishMessage(IntPtr broker, IntPtr sourceModule, byte[] message, int size) { return true; }
        public abstract bool PublishMessage(IntPtr broker, IntPtr sourceModule, byte[] message);

    }
}
