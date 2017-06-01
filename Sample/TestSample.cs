using EGEdge.IoT.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class TestSample
    {
        public static void Test()
        {
            string testConfig = "{\"modules\":" +
                " [" +
                "{\"name\": \"TestModuleA\"," +
                "\"loader\": " +
                "{\"name\": \"uwp\",\"entrypoint\":" +
                " {\"module.path\": \"TestModuleA\"}}," +
                "\"args\": {\"data\": \"abcdefg\"}}" +
                ",{\"name\": \"TestModuleB\"," +
                "\"loader\": " +
                "{\"name\": \"uwp\",\"entrypoint\":" +
                " {\"module.path\": \"TestModuleB\"}}," +
                "\"args\": {\"filename\": \"log.txt\"}}" +
              ",{\"name\": \"TestModuleC\"," +
                "\"loader\": " +
                "{\"name\": \"uwp\",\"entrypoint\":" +
                " {\"module.path\": \"TestModuleC\"}}," +
                "\"args\": {\"xxx\": \"12.34\"}}" +
                "]," +
                "\"links\": [{\"source\": \"TestModuleC\",\"sink\": \"TestModuleB\"}]" +
                "}";
            var gateway = new Gateway();
            var moduleTestA = new TestModuleA.TestModuleA();
            var moduleTestB = new TestModuleB.TestModuleB();
            var moduleTestC = new TestModuleC.TestModuleC();

            gateway.AddInitialModule("TestModuleA", moduleTestA);
            gateway.AddInitialModule("TestModuleB", moduleTestB);
            gateway.AddInitialModule("TestModuleC", moduleTestC);

            gateway.CreateFromJson(testConfig);

        }
    }
}
