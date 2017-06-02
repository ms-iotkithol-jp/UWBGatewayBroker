using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGEdge.IoT.Gateway
{
    public class Message 
    {
        public Message(byte[] msgAsByteArray)
        {
            message = new byte[msgAsByteArray.Length];
            msgAsByteArray.CopyTo(message, 0);
            this.properties = new Dictionary<string, string>();
        }   
        public Message(Message message)
        {
            this.message = new byte[message.message.Length];
            message.message.CopyTo(this.message, 0);
            this.properties = new Dictionary<string, string>();
            foreach (var key in message.properties.Keys)
            {
                this.properties.Add(key, message.properties[key]);
            }
        }
        public Message(byte[] contentAsByteArray, Dictionary<string, string> properties)
        {
            this.message = new byte[contentAsByteArray.Length];
            contentAsByteArray.CopyTo(this.message, 0);
            this.properties = new Dictionary<string, string>();
            foreach (var k in properties.Keys)
            {
                this.properties.Add(k, properties[k]);
            }
        }
        public Message(string content, Dictionary<string, string> properties)
        {
            this.message = Encoding.UTF8.GetBytes(content);
            this.properties = new Dictionary<string, string>();
            foreach (var k in properties.Keys)
            {
                this.properties.Add(k, properties[k]);
            }
        }

        public byte[] Content { get { return message; } }
        public Dictionary<string, string> Properties { get { return properties; } }

        public byte[] ToByteArray() { return message; }

        private Dictionary<string, string> properties = new Dictionary<string, string>();
        private byte[] message;
    }
}
