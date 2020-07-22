using System;
using System.Collections.Generic;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            StockService stockService = new StockService();

            BuyStock buyStock = new BuyStock(stockService);

            SellStock sellStock = new SellStock(stockService);

            StockController controller = new StockController();
            controller.AddOrder(buyStock);
            controller.AddOrder(sellStock);
            controller.AddOrder(buyStock);
            controller.AddOrder(sellStock);

            controller.ExecuteOrders();
            Console.Read();
        }
    }

    class StockService
    {
        private string productName = "Computer";
        private int _quantity = 10;

        public void Buy()
        {
            Console.WriteLine("Product: {0} , {1} item bought", productName, _quantity++);
        }

        public void Sell()
        {
            Console.WriteLine("Product: {0} , {1} item sold", productName, _quantity--);
        }
    }

    interface IOrder
    {
        void Execute();
    }

    class BuyStock : IOrder
    {
        private StockService _stockService;
        public BuyStock(StockService stockService)
        {
            _stockService = stockService;
        }

        public void Execute()
        {
            _stockService.Buy();
        }
    }

    class SellStock : IOrder
    {
        private StockService _stockService;
        public SellStock(StockService stockService)
        {
            _stockService = stockService;
        }

        public void Execute()
        {
            _stockService.Sell();
        }
    }

    class StockController
    {
        List<IOrder> _orders = new List<IOrder>();

        public void AddOrder(IOrder order)
        {
            _orders.Add(order);
        }

        public void RemoveOrder(IOrder order)
        {
            _orders.Remove(order);
        }

        public void ExecuteOrders()
        {
            foreach (var order in _orders)
            {
                order.Execute();
            }
            _orders.Clear();
        }
    }
}
