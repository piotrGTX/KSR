using Calculator;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.ServiceModel.Web;

namespace Client {
    class Program
    {
        static void Main()
        {
            standardClient(new EndpointAddress("http://localhost:2200/service"), (client) => callAction(client, "120", "100"));
            discoveryClient((client) => callAction(client, "240", "100"));

            Console.ReadKey();
        }

        static void callAction(ICalculator client, string a, string b)
        {
            Console.WriteLine("Call action");

            double resultA = client.Add(a, b);
            double resultB = client.Sub(a, b);

            Console.WriteLine("{0} + {1} = {2}", a, b, resultA);
            Console.WriteLine("{0} - {1} = {2}", a, b, resultB);
        }

        static void standardClient(EndpointAddress endpointAddress, Action<ICalculator> func)
        {
            executeClient(() =>
            {
                var factory = new ChannelFactory<ICalculator>(
                    new WebHttpBinding(),
                    endpointAddress
                );
                factory.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());

                return factory;
            }, (factory) =>
            {
                return factory.CreateChannel();
            },
            func);
        }

        static void discoveryClient(Action<ICalculator> func)
        {
            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint("soap.udp://localhost:54321"));

            var clients = discoveryClient.Find(new FindCriteria(typeof(ICalculator))).Endpoints;

            Console.WriteLine("Find {0} endpoints", clients.Count);

            if (clients.Count > 0)
            {
                EndpointAddress addr = clients[0].Address;
                Console.WriteLine("Znaleziono klienta {0}", addr);
                standardClient(addr, func);
            }
            else
            {
                Console.WriteLine("Brak klienta");
            }
        }
            

        static void executeClient(Func<ChannelFactory<ICalculator>> getFactory, Func<ChannelFactory<ICalculator>, ICalculator> getClient, Action<ICalculator> func)
        {
            ChannelFactory<ICalculator> factory = null;
            ICalculator client = null;

            try
            {
                factory = getFactory();
                client = getClient(factory);

                if (client != null)
                {
                    func(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (client != null)
                {
                    ((IDisposable)client).Dispose();
                }
                if (factory != null)
                {
                    factory.Close();
                }
            }
        }

    }
   
}
