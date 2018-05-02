using System;

namespace GeneticAlgorithm
{
    class PopulationParameters: IComparable<PopulationParameters>
    {
        public double Fitness { get; set; }
        public double[] Args;
        public delegate double CountСhromosome(double op1, double op2);
        public static readonly double ElRate = 0.3;                  

        public PopulationParameters(double fit, double [] buf)
        {
            Args = new double[buf.Length];
            Fitness = fit;
            buf.CopyTo(Args,0);
        }

        public void PrintArgs()
        {
            Console.WriteLine("---------");
            for (int i = 0; i < Args.Length; i++)
                Console.Write($"[{Args[i]}]");
        }

        public int CompareTo(PopulationParameters p)           
        {
            if (this.Fitness > p.Fitness)
                return 1;
            if (this.Fitness < p.Fitness)
                return -1;
            else
                return 0;
        }

        public PopulationParameters Clone()
        {
            return new PopulationParameters(Fitness, Args);
        }

        public double [] Child(CountСhromosome f, PopulationParameters p, IFunction funct)
        {
            double[] buf = new double[Args.Length];
            double tmp;
            for(int i=0;i<Args.Length;i++)
            {
                tmp = f(Args[i],p.Args[i]);
                if (tmp < funct.BeginInterval)
                    buf[i] = funct.BeginInterval;
                else
                    if (tmp > funct.EndInterval)
                    buf[i] = funct.EndInterval;
                else
                    buf[i] = tmp;
            }
            return buf;
        }

        public double [] Mutate(Random rand,IFunction f)
        {
            double[] buf = new double[Args.Length];
            for (int i = 0; i < Args.Length; i++)
            {
                double tmp = buf[i];
                if (rand.Next(2) == 1)
                    tmp += rand.NextDouble();
                else
                    tmp -= rand.NextDouble();
                if (tmp >= f.BeginInterval && tmp <= f.EndInterval)
                    buf[i] = tmp;
            }
            return buf;
        }

    }
}
