using System;

namespace AbstractFactory
{

    /// <summary>
    /// Amaç: Factory design patern’e ek olarak, toplu nesne kullanımı ihtiyaçlarında yönetilebilir bir yapı sunmak amaçlanmaktadır. 
    /// Örneğin, Loglama veya cache yapıları farklı şekillerde yapılabilmektedir. Loglama yaparken veritabanına, dosyaya veya consola loglama yapabiliriz. 
    /// Aynı şekilde cache için de MemoryCache, MemCache, RedisCache gibi farklı yöntemler kullanabiliriz. İş ihtiyaçları için de farklı yöntemler izleyebiliriz. 
    /// Örneğin evrak takip sisteminde kimi kullanıcılar için mail atabilirken kimileri için sms gönderebiliriz, yada sadece uyarı çıkarabiliriz. 
    /// Bu gibi çoklu nesnelerin oluşturulmasının yönetiminde abstract factory patern kullanabiliriz.
    /// </summary>
    /// <example>
    /// <code>
    /// public abstract class CrossCuttingConcernFactory
    /// {
    /// public abstract Logging CreateLogger();
    /// public abstract Caching CreateCaching();
    /// }
    /// </code>
    /// </example>

    class Program
    {
        static void Main(string[] args)
        {

            var productManager = new ProductManager(new Factory2());
            productManager.GetAll();

            Console.Read();
        }
    }


    public class ProductManager
    {
        CrossCuttingConcernFactory crossCuttingConcernFactory;

        Logging logging;
        Caching caching;

        public ProductManager(CrossCuttingConcernFactory crossCuttingConcernFactory)
        {
            this.crossCuttingConcernFactory = crossCuttingConcernFactory;
            this.logging = this.crossCuttingConcernFactory.CreateLogger();
            this.caching = this.crossCuttingConcernFactory.CreateCaching();
        }

        public void GetAll()
        {
            logging.Log();
            caching.Cache();
            Console.WriteLine("All products are fetched");
        }
    }

    public abstract class Logging
    {
        public abstract void Log();
    }

    public class Log4netLogger : Logging
    {
        public override void Log()
        {
            Console.WriteLine("Logged with Log4Net");
        }
    }

    public class NLogLogger : Logging
    {
        public override void Log()
        {
            Console.WriteLine("Logged with NLogLogger");
        }
    }

    public abstract class Caching
    {
        public abstract void Cache();
    }

    public class MemCache : Caching
    {
        public override void Cache()
        {
            Console.WriteLine("Cache with MemCache");
        }
    }

    public class RedicCache : Caching
    {
        public override void Cache()
        {
            Console.WriteLine("Cache with RedicCache");
        }
    }

    public abstract class CrossCuttingConcernFactory
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernFactory
    {
        public override Caching CreateCaching()
        {
            return new RedicCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4netLogger();
        }
    }

    public class Factory2 : CrossCuttingConcernFactory
    {
        public override Caching CreateCaching()
        {
            return new MemCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogLogger();
        }
    }
}
