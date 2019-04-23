using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Calculator
{
  
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        [WebGet(UriTemplate = "add/{a}/{b}", ResponseFormat = WebMessageFormat.Json)]
        double Add(string a, string b);

        [OperationContract]
        [WebGet(UriTemplate = "sub/{a}/{b}", ResponseFormat = WebMessageFormat.Json)]
        double Sub(string a, string b);

        [OperationContract]
        [WebInvoke(UriTemplate = "form", Method = "POST")]
        string formularz(Stream stream);
    }


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Calculator : ICalculator
    {
        private double secret = 0;

        public double Add(string a, string b)
        {
            return double.Parse(a) + double.Parse(b);
        }

        public string formularz(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            return "Form: " + streamReader.ReadToEnd();
        }

        public double Sub(string a, string b)
        {
            return double.Parse(a) - double.Parse(b);
        }

        public void ustawSekret(double newSecret)
        {
            if (secret == 0)
            {
                secret = newSecret;
            }
        }
    }

}
