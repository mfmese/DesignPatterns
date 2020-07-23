using System;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductServiceTests test = new ProductServiceTests();
            test.Saving_Test();

            Console.Read();
        }
    }

    class ProductService
    {
        private ILogger _logger;

        public ProductService(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            _logger.Log();
            Console.WriteLine("Saved");
        }
    }
    interface ILogger
    {
        void Log();
    }

    class Log4NetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4Net");
        }
    }

    class EdLogger: ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }

    class StubLogger : ILogger
    {
        private static StubLogger _stubLogger;
        private StubLogger() { }
        private static object _lock = new object();

        public static StubLogger CreateSingleton()
        {
            lock (_lock)
            {
                _stubLogger = _stubLogger == null ? new StubLogger() : _stubLogger;
            }
            return _stubLogger;
        }
        public void Log()
        {
            
        }
    }

    class ProductServiceTests
    {
        public void Saving_Test()
        {
            ProductService service = new ProductService(StubLogger.CreateSingleton());
            service.Save();
        }
    }
}
