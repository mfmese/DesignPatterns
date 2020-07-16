using System;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Scorring scorring;
            Console.WriteLine("Hard");
            scorring = new HardScorring();
            Console.WriteLine(scorring.Generate(5, new TimeSpan(0, 2, 20)));

            Console.WriteLine("Medium");
            scorring = new MediumScorring();
            Console.WriteLine(scorring.Generate(5, new TimeSpan(0, 2, 20)));

            Console.Read();
        }
    }

    abstract class Scorring
    {
        public int Generate(int hits, TimeSpan time)
        {
            int score = CalculateScore(hits);
            int reduction = CalculateReduction(time);

            return CalculateTotalScore(score, reduction);
        }

        public abstract int CalculateTotalScore(int score, int reduction);

        public abstract int CalculateScore(int hits);

        public abstract int CalculateReduction(TimeSpan time);
    }

    class HardScorring : Scorring
    {
        public override int CalculateReduction(TimeSpan time)
        {
            return (int)time.TotalSeconds / 3;
        }

        public override int CalculateScore(int hits)
        {
            return hits * 100;
        }

        public override int CalculateTotalScore(int score, int reduction)
        {
            return score - reduction;
        }
    }

    class MediumScorring : Scorring
    {
        public override int CalculateReduction(TimeSpan time)
        {
            return (int)time.TotalSeconds / 5;
        }

        public override int CalculateScore(int hits)
        {
            return hits * 80;
        }

        public override int CalculateTotalScore(int score, int reduction)
        {
            return score - reduction;
        }
    }
}
