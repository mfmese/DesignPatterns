using System;
using System.Collections.Generic;
using System.Threading;

namespace Mediator
{
    /// <summary>
    /// Amaç: Arabulucu deseni olarak bilinir. Nesneleri bir biri ile görüştürmek için kullanılır. Örneğin uçaklar için kontrol kulesi bir Mediator diyebiliriz.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IstanbulControlMediator istanbulAirTower = new IstanbulControlMediator();

            HakikiAirline f16 = new HakikiAirline { Airport = istanbulAirTower, FlightNumber = "f16", From = "Istanbul" };
            istanbulAirTower.Register(f16);

            OzHakikiAirline f35 = new OzHakikiAirline { Airport = istanbulAirTower, FlightNumber = "f35", From = "Ankara" };
            istanbulAirTower.Register(f35);

            f16.RequestNewWay("33:55E -  21:80W");

            f35.RequestNewWay("33:55E -  21:80W");

            Console.Read();
        }
    }

    abstract class Airline
    {
        public IAirportControl Airport { get; set; }
        public string FlightNumber { get; set; }
        public string From { get; set; }

        public void RequestNewWay(string way)
        {
            Console.WriteLine("Requested way from {0}, way is {1}", this.ToString(), way);
            Airport.SuggestWay(FlightNumber, way);
        }

        public virtual void GetWay(string messageFromAirport)
        {
            Console.WriteLine("{0} rotasına yöneliniz", messageFromAirport);
        }
    }

    class HakikiAirline: Airline
    {
        public override void GetWay(string messageFromAirport)
        {
            Console.WriteLine("Hakiki Airline Flight {0}", FlightNumber);

            base.GetWay(messageFromAirport);
        }

        public override string ToString()
        {
            return "HakikiAirline";
        }
    }

    class OzHakikiAirline : Airline
    {
        public override void GetWay(string messageFromAirport)
        {
            Console.WriteLine("Oz Hakiki Airline Flight {0}", FlightNumber);

            base.GetWay(messageFromAirport);
        }

        public override string ToString()
        {
            return "OzHakikiAirline";
        }
    }

    interface IAirportControl
    {
        void Register(Airline airline);
        void SuggestWay(string flightNumber, string way);
    }
    
    class IstanbulControlMediator : IAirportControl
    {
        private  Dictionary<string, Airline> _planes;

        public IstanbulControlMediator()
        {
            _planes = new Dictionary<string, Airline>();
        }

        public void Register(Airline airline)
        {
            if (!_planes.ContainsValue(airline))
            {
                _planes[airline.FlightNumber] = airline;
            }
            airline.Airport = this;
        }

        public void SuggestWay(string flightNumber, string way)
        {
            Thread.Sleep(300);
            Random random = new Random();

            string messageFromAirport = string.Format("{0}:{1}E - {2}:{3}W", random.Next(1, 100).ToString(), random.Next(1, 100).ToString(), random.Next(1, 100).ToString(), random.Next(1, 100).ToString());

            _planes[flightNumber].GetWay(messageFromAirport);
        }
    }
}
