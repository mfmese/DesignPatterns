using System;

namespace Builder
{
    /// <summary>
    /// Amaç: Bir nesne örneği çıkarmak için kullanılır. Nesne örneği birbiri ardına atılacak olan adımların sonrasında ortaya çıkmaktadır. 
    /// İş katmanında if blokları ile yönetmek yerine Builder deseni kullanılabilir. Gerçek yaşam örneği olarak hamburger üretim süreçlerine benzetim yapılabilir. 
    /// Hamburger bir nesne ise vejeteryan için ayrı, vejeteryan olmayanlar için ayrı bir süreç sonucu hamberger nesnesi üretilebilmektedir. 
    /// Özellikle front-end development da kullanılan bir design pattern dir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var productBuilder = new OldCustomerProductBuilder();

            var productDirector = new ProductDirector(productBuilder);
      
            var product = productDirector.GetProduct();

            Console.WriteLine(product.Id);
            Console.WriteLine(product.CategoryName);
            Console.WriteLine(product.ProductName);
            Console.WriteLine(product.UnitPrice);
            Console.WriteLine(product.DiscountApplied);
            Console.WriteLine(product.DiscountedPrice);
  
            Console.Read();
        }
    }

    class ProductViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool DiscountApplied { get; set; }
    }

    class ProductDirector
    {
        private ProductBuilder productBuilder;

        public ProductDirector(ProductBuilder productBuilder)
        {
            this.productBuilder = productBuilder;
        }

        public void GenerateProduct()
        {
            productBuilder.GetProductData();
            productBuilder.ApplyDiscount();
        }  
        
        public ProductViewModel GetProduct()
        {
            return productBuilder.GetViewModel();
        }
    }

    abstract class ProductBuilder
    {
        public abstract void GetProductData();
        public abstract void ApplyDiscount();
        public abstract ProductViewModel GetViewModel();
    }

    class NewCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();

        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice * (decimal)0.9;
            model.DiscountApplied = true;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override ProductViewModel GetViewModel()
        {
            return model;
        }
    }

    class OldCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();

        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice;
            model.DiscountApplied = false;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override ProductViewModel GetViewModel()
        {
            return model;
        }
    }


}
