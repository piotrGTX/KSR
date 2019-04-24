using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.ServiceModel.Web;

namespace Client
{    class Program
    {
        static void Main()
        {
            
            var host = new ServiceHost(typeof(Calculator.Calculator), new Uri("http://localhost:2200"));

            host.AddServiceEndpoint(
                typeof(Calculator.ICalculator), 
                new WebHttpBinding(),
                "service"
            ).Behaviors.Add(new WebHttpBehavior());
            
            //var host = new WebServiceHost(typeof(Calculator.ICalculator), new Uri("http://localhost:2200/service"));

            host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            host.AddServiceEndpoint(new UdpDiscoveryEndpoint("soap.udp://localhost:54321"));

            host.Open();
            Console.WriteLine("The Calculator Server starts !");
            Console.ReadKey();
            host.Close();
        }
    }
}
