using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Calculator
{

    public interface IZwrotny
    {
        [OperationContract(IsOneWay = true)]
        void UstawProcent(int a);

        [OperationContract(IsOneWay = true)]

        void UstawWynik(double a);
    }

    [ServiceContract(CallbackContract = typeof(IZwrotny))]
    public interface ICalculator
    {
        /*
        [OperationContract]
        int Add(int a, int b);
        [OperationContract]
        int Eval(Data data);
        */

        [OperationContract(IsOneWay = true)]
        void WykonajObliczenia();

        [OperationContract(IsOneWay = true)]
        void Dodaj(double a, double b);

        [OperationContract(IsOneWay = true)]
        void ustawSekret(double newSecret);
    }

    [DataContract]
    public class Data
    {
        [DataMember]
        public int arg1 { get; set; }
        [DataMember]
        public int arg2 { get; set; }
        [DataMember]
        public char op { get; set; }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Calculator : ICalculator
    {
        /*
        public int Add(int a, int b)
        {
            if (a == b)
            {
                throw new FaultException("Same numbers");
            }
            return a + b + 1;
        }

        public int Eval(Data data)
        {
            switch(data.op)
            {
                case '-': return data.arg1 - data.arg2;
                case '*': return data.arg1 * data.arg2;
                case '/': return data.arg1 / data.arg2;
            }
            return data.arg1 + data.arg2;
        }
        */

        private double secret = 0;

        public void WykonajObliczenia()
        {
            var zw = OperationContext.Current.GetCallbackChannel<IZwrotny>();
            zw.UstawProcent((int) secret);
        }
         
        public void Dodaj(double a, double b)
        {
            double wynik = a + b;

            var zw = OperationContext.Current.GetCallbackChannel<IZwrotny>();
            zw.UstawWynik(wynik);
           
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
