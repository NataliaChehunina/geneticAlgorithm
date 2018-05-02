using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    class GenAlgorithm
    {
        readonly int PopulationQuantity;
        readonly int Generations;
        Random rand;
        readonly int ElIndex;
        readonly int RestIndex;
        IFunction Funct;
        List<PopulationParameters> Population, Buffer;

        public GenAlgorithm(IFunction f,int popul_quant, int gen)
        {
            PopulationQuantity = popul_quant;
            Funct = f;
            Population = new List<PopulationParameters>();
            rand = new Random();
            Generations = gen;
            Buffer = new List<PopulationParameters>();
            ElIndex = (int) (PopulationQuantity * PopulationParameters.ElRate);
            RestIndex = PopulationQuantity - ElIndex;
        }

        void RandomGenPopulation()
        {
            for(int i=0;i<PopulationQuantity;i++)
            {
                double [] buf = new double [Funct.XQuant] ;
                for (int j = 0; j < Funct.XQuant; j++)
                    buf[j] = rand.NextDouble() + rand.Next((int)Funct.BeginInterval, 
                        (int)Funct.EndInterval);
                Population.Add(new PopulationParameters(0,buf));
            }
        }

        void CountFitness()
        {
            for (int i = 0; i < PopulationQuantity; i++) 
                Population[i].Fitness = Funct.Function(
                    Population[i].Args);
        }

        PopulationParameters Max (PopulationParameters p1, PopulationParameters p2)
        {
            if (p1.CompareTo(p2) >= 0)
                return p1;
            return p2;
        }

        PopulationParameters Min(PopulationParameters p1, PopulationParameters p2)
        {
            if (p1.CompareTo(p2) <= 0)
                return p1;
            return p2;
        }

        PopulationParameters Sorting()
        {
            Population.Sort();
            if (Funct.Max == true)
                Population.Reverse();
            return Population[0];
        }

        void Elite()
        {
            Population.RemoveRange(ElIndex, RestIndex);
        }

        void Mutate()
        {
            for(int i=Buffer.Count; i<PopulationQuantity;i++)
            {
                Buffer.Add(new PopulationParameters(0, 
                    Buffer[rand.Next(Buffer.Count)].Mutate(rand,Funct)));
                Buffer[i].Fitness = Funct.Function(Buffer[i].Args);
            }             
        }

        void LinearCrossover()
        {
            Buffer.Clear();
            for (int i=0;i<Population.Count - 1;i++)
            {
                var Children = Tuple.Create(new PopulationParameters
                    (0, Population[i].Child((x,y)=>(x+y)/2, Population[i+1], Funct)),
                    new PopulationParameters(0, Population[i].Child((x,y) => (3*x-y)/2, Population[i+1], Funct)),
                    new PopulationParameters(0, Population[i].Child((x,y) => (-x+3*y)/2, Population[i + 1], Funct)));
                Children.Item1.Fitness = Funct.Function(Children.Item1.Args);
                Children.Item2.Fitness = Funct.Function(Children.Item2.Args);
                Children.Item3.Fitness = Funct.Function(Children.Item3.Args);
                var BestChildren = Tournament(Children);
                Buffer.Add(BestChildren.Item1);
                Buffer.Add(BestChildren.Item2);
            }
        }

        Tuple<PopulationParameters, PopulationParameters> Tournament(
            Tuple<PopulationParameters, PopulationParameters, PopulationParameters> p)
        {
            if (Funct.Max == true)
                return new Tuple<PopulationParameters, PopulationParameters>
                    (Max(p.Item1, p.Item2), Max(p.Item3, Min(p.Item1, p.Item2)));
            return new Tuple<PopulationParameters, PopulationParameters>
                (Min(p.Item1, p.Item2),Min(p.Item3,Max(p.Item1,p.Item2)));
        }

        void NextGen()
        {
            Population.Clear();
            for (int i = 0; i < Buffer.Count; i++)                               
                Population.Add(Buffer[i].Clone());
            Buffer.Clear();
        }

        public void Result()                               
        {
            RandomGenPopulation();
            CountFitness();
            for (int i=0;i<Generations;i++)
            {
                PopulationParameters elem = Sorting();
                CountFitness();
                Elite();
                LinearCrossover();
                Mutate();
                NextGen();
            }
            Sorting();
            Population[0].PrintArgs();
            Console.WriteLine($"____{Population[0].Fitness}____");
        }

    }
}
