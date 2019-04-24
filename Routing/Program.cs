using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace Routing
{
    class Program
    {
        static void Main()
        {
            var client1Address = "http://localhost:2200/service";
            var client2Address = "http://localhost:3300/service";

            var routerAddr = "http://localhost:1000/router";

            var h = new ServiceHost(typeof(RoutingService));
            h.AddServiceEndpoint(typeof(IRequestReplyRouter), new BasicHttpBinding(), routerAddr);
            var rc = new RoutingConfiguration();
           
            var lst = new List<ServiceEndpoint>();
            addClient(lst, client1Address);
            addClient(lst, client2Address);

            rc.FilterTable.Add(new MatchAllMessageFilter(), lst);
            h.Description.Behaviors.Add(new RoutingBehavior(rc));

            h.Open();

            Console.WriteLine("Working");
            Console.ReadKey();

            h.Close();
        }

        static void addClient(List<ServiceEndpoint> list, string address)
        {
            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));
            var client = new ServiceEndpoint(contract, new BasicHttpBinding(), new EndpointAddress(address));
            list.Add(client);
        }
    }
}
