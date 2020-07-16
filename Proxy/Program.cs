using System;
using System.Threading;

namespace Proxy
{
    /// <summary>
    /// Amaç: Cache’leme sistemine benzetebiliriz. İlk kez çağrıldığında üzerine düşen işlemi yapar ama ikinci kez tekrar çağrılırsa ilk çağrılanı kullanmayı amaçlar. 
    /// Bir cache mekanizması olarak tanımlanabilir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CustomerBase mng = new CustomerManagerProxy();

            Console.WriteLine(mng.Calculate());
            Console.WriteLine(mng.Calculate());

            Console.Read();
        }
    }

    abstract class CustomerBase
    {
        public abstract int Calculate();
    }


    class CustomerManager : CustomerBase
    {
        public override int Calculate()
        {
            int result = 1;
            for (int i = 1; i <= 5; i++)
            {
                result *= i;
                Thread.Sleep(1000);
            }
            return result;
        }
    }

    class CustomerManagerProxy : CustomerBase
    {
        private CustomerBase _customerManager;
        private int _result;

        public override int Calculate()
        {
            if(_customerManager == null)
            {
                _customerManager = new CustomerManager();
                _result = _customerManager.Calculate();
            }

            return _result;
        }
    }
}
