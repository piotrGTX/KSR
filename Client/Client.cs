using System;
using System.ServiceModel;

namespace Client {

    class Program
    {
        static void Main()
        {
            var factory = new ChannelFactory<ServerService.ICalculator>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/wcfpipe"));
            var client = factory.CreateChannel();

            Console.WriteLine("PIPE");
            Console.WriteLine("Podaj dwie liczby:");
            string a = Console.ReadLine();
            string b = Console.ReadLine();
            double resultA = client.Add(a, b);
            double resultB = client.Sub(a, b);

            Console.WriteLine("{0} + {1} = {2}", a, b, resultA);
            Console.WriteLine("{0} - {1} = {2}", a, b, resultB);

            Console.ReadKey();

            factory.Close();
            ((IDisposable)client).Dispose();
        }
    }
   
}
