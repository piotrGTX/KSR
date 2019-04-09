using System;
using System.ServiceModel;

namespace Client
{
    class Handler : ServerService.ICalculatorCallback
    {
        public void UstawProcent(int a)
        {
            Console.WriteLine("Sekret wynosi: {0}", a);
        }

        public void UstawWynik(double wynik)
        {
            Console.WriteLine("Wynik operacji to: {0}", wynik);
        }
    }

    class Program
    {
        static void Main()
        {
            var factory = new DuplexChannelFactory<ServerService.ICalculator>(new Handler(), new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/wcfpipe"));
            var client = factory.CreateChannel();

            Console.Write("Podaj sekret: ");
            int secret = Convert.ToInt32(Console.ReadLine());
            client.ustawSekret(secret);

            client.Dodaj(12, 100);
            client.WykonajObliczenia();

            Console.ReadKey();

            factory.Close();
            ((IDisposable)client).Dispose();
        }
    }
   
}
