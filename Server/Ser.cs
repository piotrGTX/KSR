using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Client
{
    class Program
    {
        static void Main()
        {
            var host = new ServiceHost(typeof(Calculator.Calculator), new Uri[] { new Uri("http://localhost:1100") });

            var serverMetadata = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serverMetadata == null)
            {
                serverMetadata = new ServiceMetadataBehavior();
            }
            host.Description.Behaviors.Add(serverMetadata);

            host.AddServiceEndpoint(
                typeof(Calculator.ICalculator), 
                new NetNamedPipeBinding(), 
                "net.pipe://localhost/wcfpipe"
            );
            host.AddServiceEndpoint(
                ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadane"
            );

            host.Open();
            Console.WriteLine("The Calculator Server starts !");
            Console.ReadKey();
            host.Close();
        }
    }
}
