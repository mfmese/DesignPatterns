using System;

namespace Facade
{
    /// <summary>
    /// Amaç: Ortak amaçlar için kullanılan birçok sınıf olabilir. Sınıfların yönetimini kolaylaştırmak için tüm ortaklaşan sınıfları tek bir cephe de toplayıp 
    /// oradan kullanılmasını sağlayan bir yapı sunmaktadır. Özellikle Uygulamayı dikine kesen CrossCuttingConcern (Logging, Caching, Authorization) 
    /// durumlarında kullanılmasının yönetimsel açıdan avantajlarını sunmaktadır.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var customerManager = new CustomerManager();
            customerManager.Save();

            Console.Read();
        }
    }

    public class CustomerManager
    {
        readonly CrossCuttingConcernFacade concerns;
        public CustomerManager()
        {
            concerns = new CrossCuttingConcernFacade();
        }

        public void Save()
        {
            concerns.Logging.Log();
            concerns.Caching.Cache();
            concerns.Authorize.Authorize();
            Console.WriteLine("Saved");
        }
    }

    class CrossCuttingConcernFacade
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorize Authorize;

        public CrossCuttingConcernFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorize = new Authorize();
        }
    }

    interface ILogging
    {
        void Log();
    }
    interface ICaching
    {
        void Cache();
    }
    interface IAuthorize
    {
        void Authorize();
    }

    class Logging : ILogging
    {
        public void Log()
        {
            Console.WriteLine("logged");
        }
    }

    class Caching : ICaching
    {
        public void Cache()
        {
            Console.WriteLine("cached");
        }
    }

    class Authorize : IAuthorize
    {
        void IAuthorize.Authorize()
        {
            Console.WriteLine("Authorized");
        }
    }
}
