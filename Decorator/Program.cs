using System;

namespace Decorator
{
    /// <summary>
    /// Amaç: Runtime da bir nesneye yeni özellikler eklemek istenirse kullanılır. 
    /// Farklı zamanlarda farklı kullanıcılara farklı şekillerde ürünü sunmak istediğimizde decorator design patern kullanırız.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CarBase personelCar = new PersonalCar() { Make = "Citroen", Model = "C3", HirePrice = 100 };

            SpecialOffer specialOffer = new SpecialOffer(personelCar);

            specialOffer.DiscountPercentage = 10;

            Console.WriteLine("Special Offer: Make: {0}, Model:{1}, HirePrice: {2}", specialOffer.Make, specialOffer.Model, specialOffer.HirePrice);

            Console.Read();
        }
    }

    abstract class CarBase
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public abstract decimal HirePrice { get; set; }
    }

    class PersonalCar : CarBase
    {
        public override decimal HirePrice { get; set; }
    }

    class CommercialCar : CarBase
    {
        public override decimal HirePrice { get; set; }
    }

    class CarDecorator : CarBase
    {
        private readonly CarBase carBase;

        public CarDecorator(CarBase carBase)
        {
            this.carBase = carBase;
        }

        public override decimal HirePrice { get; set; }
    }

    class SpecialOffer : CarDecorator
    {
        public int DiscountPercentage { get; set; }

        private readonly CarBase carBase;

        public SpecialOffer(CarBase carBase) : base(carBase)
        {
            this.carBase = carBase;
        }

        public override decimal HirePrice { get { return carBase.HirePrice - (carBase.HirePrice * DiscountPercentage / 100);  } set { } }
    }

    class OtherOffer : CarDecorator
    {
        private readonly CarBase carBase;

        public OtherOffer(CarBase carBase) : base(carBase)
        {
            this.carBase = carBase;
        }

        public override decimal HirePrice { get { return carBase.HirePrice - (carBase.HirePrice * 10 / 100); } set { } }
    }
}
