using System;
using System.Collections.Generic;

namespace Observer
{
    /// <summary>
    /// Amaç: one-to-many ilişki olan nesnelerde kullanılır. Eğer nesne değişmişse ona bağlı diğer nesneler otomatik olarak bilgilendirilir. 
    /// Kendine abone olan sistemlerin bir işlem olduğunda devreye girmesini sağlayan bir desendir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager();
            productManager.Attach(new EmployeeObserver());
            productManager.Attach(new CustomerObserver());
            productManager.UpdatePrice();

            Console.Read();
        }
    }

    class ProductManager
    {
        List<Observer> _observers = new List<Observer>();

        public void UpdatePrice()
        {
            Console.WriteLine("Product price changed");
            Notify();
        }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        private void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }

    abstract class Observer
    {
        public abstract void Update();
    }

    class CustomerObserver : Observer
    {
        public override void Update()
        {
            Console.WriteLine("Message to Customer: Product price updated");
        }
    }

    class EmployeeObserver : Observer
    {
        public override void Update()
        {
            Console.WriteLine("Message to Employee: Product price updated");
        }
    }
}
