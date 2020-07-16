using System;
using System.Security.Principal;

namespace ChainOfResponsibility
{
    /// <summary>
    /// Amaç: Belirli bir şarta göre hangi nesneyi kullanmamız gerektiğini davranışsal olarak gösteren desendir. Hiyerarşik bir yapıda olması ise bu desenin en temel özelliğidir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            VisePresident visePresident = new VisePresident();
            President president = new President();

            manager.SetSuccessor(visePresident);
            visePresident.SetSuccessor(president);

            Expense expense = new Expense { Amount = 110, Detail = "Fuel expense" };

            manager.HandleExpense(expense);

            Console.Read();
        }
    }

    class Expense
    {
        public string Detail { get; set; }
        public decimal Amount { get; set; }
    }

    abstract class ExpenseHandlerBase
    {
        protected ExpenseHandlerBase Successor;

        public abstract void HandleExpense(Expense expense);

        public void SetSuccessor(ExpenseHandlerBase successor)
        {
            Successor = successor;
        }
    }

    class Manager : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if(expense.Amount < 100)
            {
                Console.WriteLine("Manager handle the expense");
            }
            else if(Successor != null)
            {
                Successor.HandleExpense(expense);
            }
        }
    }
    class VisePresident : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount >= 100 && expense.Amount < 1000)
            {
                Console.WriteLine("VisePresident handle the expense");
            }
            else if (Successor != null)
            {
                Successor.HandleExpense(expense);
            }
        }
    }
    class President : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount > 1000)
            {
                Console.WriteLine("President handle the expense");
            }
        }
    }
}
