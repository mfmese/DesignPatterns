using System;

namespace Prototype
{
    /// <summary>
    /// Amaç: Nesne üretim maliyetlerini minimize etmek için kullanılır. Nesne clone yapmak için kullanılır. 
    /// Özellikle çok property sahip bir nesnenin sadece birkaç property içeriğini değiştirerek yeni bir nesne oluşturmak istediğimizde bize büyük bir avantaj sağlar.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer { FirstName = "Fethi", LastName = "Meşe", City = "Istanbul", Id = 1 };       

            var customer2 = (Customer)customer.Clone();
            customer2.FirstName = "Mehmet";

            Console.WriteLine(customer.FirstName);
            Console.WriteLine(customer2.FirstName);

            Console.Read();
        }
    }

    public abstract class Person
    {
        public abstract Person Clone();
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Customer : Person
    {
        public string City { get; set; }

        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }

    public class Employee : Person
    {
        public decimal Salary { get; set; }

        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }
}
