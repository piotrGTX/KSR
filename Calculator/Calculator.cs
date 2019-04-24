using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

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
        double formularz(Stream stream);

        [OperationContract, WebGet(UriTemplate = "index.html"), XmlSerializerFormat]
        XmlDocument Index();
    }


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Calculator : ICalculator
    {
        private double secret = 0;

        public double Add(string a, string b)
        {
            return double.Parse(a) + double.Parse(b);
        }

        public double formularz(Stream stream)
        {
            //StreamReader streamReader = new StreamReader(stream);
            //var p = streamReader.ReadToEnd().Split('&');

            return 12;
            //return Add(p[0].Substring(6), p[1].Substring(6));
        }

        public XmlDocument Index()
        {
            var d = new XmlDocument();
            d.XmlResolver = null;
            d.LoadXml("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" " +
             "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" +
             "<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"pl\" " +
            "lang=\"pl\"><head><title>Strona testowa</title>" +
             "<meta http-equiv=\"Content-Type\" content=\"text/html; " +
             "charset=utf-8\" />" +
             "</head><body><p>Piotr Biesek - Strona testowa.</p></body></html>");
            return d;
        }    

        public double Sub(string a, string b)
        {
            return 100;
            //return double.Parse(a) - double.Parse(b);
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
