using System;

namespace Adapter
{
    /// <summary>
    /// Amaç: Farklı sistemleri kendi sistemimize entegre ederken, kendi sistemimiz bozulmadan farklı sistemlerin kullanımının sağlanmasını amaçlamaktadır.
    /// Nesnel programlama ve test edilebilirliğin sağlanmasında önemli rol oynamaktadır.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var productManager = new ProductManager(new Log4netAdapter());
            productManager.Save();
            Console.Read();
        }
    }

    class ProductManager
    {
        ILogger logger;
        public ProductManager(ILogger logger)
        {
            this.logger = logger;
        }
        public void Save()
        {
            logger.Log("new message");
            Console.WriteLine("Saved");
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class EdLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("EdLogger {0}", message);
        }
    }

    class Log4Net
    {
        public void Log(string message, string type)
        {
            Console.WriteLine("Log4Net {0}", message);

            if (!string.IsNullOrEmpty(type))
            {
                Console.WriteLine("message type is {0}", type);
            }
        }
    }

    class Log4netAdapter : ILogger
    {
        public void Log(string message)
        {
            new Log4Net().Log(message,"");
        }
    }
}
