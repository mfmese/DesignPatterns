using System;

namespace Strategy
{
    /// <summary>
    /// Amaç: if veya switch blokları yazmak yerine kullanılan bir desendir.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CreditManager mng = new CreditManager();
            mng.CreditCalculatorBase = new Before2011CreditCalculator();
            mng.Save();
            Console.Read();
        }
    }

    abstract class CreditCalculatorBase
    {
        public abstract void Calculate();
    }

    class Before2011CreditCalculator : CreditCalculatorBase
    {
        public override void Calculate()
        {
            Console.WriteLine("Credit calculated before 2011");
        }
    }

    class After2011CreditCalculator : CreditCalculatorBase
    {
        public override void Calculate()
        {
            Console.WriteLine("Credit calculated after 2011");
        }
    }

    class CreditManager
    {
        public CreditCalculatorBase CreditCalculatorBase { get; set; }
        public void Save()
        {
            Console.WriteLine("Credit manager business");
            CreditCalculatorBase.Calculate();

        }
    }

}
