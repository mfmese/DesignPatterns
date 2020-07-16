using System;

namespace Factory
{
    /// <summary>
    /// Amaç: Yazılımda değişimi kontrol altına almak için kullanılmaktadır. Veri erişim katmanında kullanılan ORM den, catch ve loglama sistemlerine, 
    /// hatta iş (business) sistemlerine kadar değişkenlik gösterebilecek durumları kontrol altına alabilmemizi sağlayacak bir yapı sunmaktadır.
    /// Kullanımı: var object = ObjectFactory.Create()
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var customerManager = new CustomerManager(new LoggerFactoryLog4Net());
            customerManager.Save();

            Console.Read();
        }
    }

    public class CustomerManager
    {
        ILoggerFactory loggerFactory;
        public CustomerManager(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public void Save()
        {
            Console.WriteLine("customer saved");

            ILogger logger = loggerFactory.CreateLogger();
            logger.Log();
        }
    }

    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new EdLogger();
        }
    }

    public class LoggerFactoryLog4Net : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new Log4NetLogger();
        }
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

    public interface ILogger
    {
        void Log();
    }

    public class EdLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }

    public class Log4NetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4NetLogger");
        }
    }
}
