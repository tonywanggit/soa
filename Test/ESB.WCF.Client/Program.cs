using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ESB.WCF.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EndpointAddress address = new EndpointAddress("http://localhost/ProviderService/Wcf/WCF_DEMO.svc");
            BasicHttpBinding binding = new BasicHttpBinding();

            ChannelFactory<IDemo> factory = new ChannelFactory<IDemo>(binding, address);

            IDemo channel = factory.CreateChannel();

            string result = channel.Echo("Tony");

            Console.WriteLine(result);
            Console.ReadLine();  
        }
    }
}
