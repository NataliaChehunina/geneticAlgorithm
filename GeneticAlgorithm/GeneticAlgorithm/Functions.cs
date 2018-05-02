using System;
using static System.Math;

namespace GeneticAlgorithm
{
    class DeJong : IFunction
    {
        public double BeginInterval { get { return -2.048; } }
        public double EndInterval { get {return 2.048; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return true; } }

        public double Function(double [] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            return 3905.93 - 100 * (X[0] * X[0] - X[1]) *
                (X[0] * X[0] - X[1]) - (1 - X[1]) * (1 - X[1]);
        }
    }

    class GoldsteinPrice : IFunction
    {
        public double BeginInterval { get { return -2; } }
        public double EndInterval { get { return 2; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            return (1 + (X[0] + X[1] + 1) * (X[0] + X[1] + 1) *
                (19 - 14 * X[0] + 3 * X[0] * X[0] - 14 * X[1] + 6 * X[0] * X[1] + 3 * X[1] * X[1])) *
                (30 + (2 * X[0] - 3 * X[1]) * (2 * X[0] - 3 * X[1]) * (18 - 32 * X[0] + 12 * X[0] * X[0] +
                48 * X[1] - 36 * X[0] * X[1] + 27 * X[1] * X[1]));
        }
    }

    class Branin : IFunction
    {
        public double BeginInterval { get { return -5; } }
        public double EndInterval { get { return 10; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            double a = 1;
            double b = 5.1 / 4 * (7.0 / 22) * (7.0 / 22);
            double c = (5 * 7) / 22.0;
            double d = 6;
            double e = 10;
            double f = 7.0 / (8 * 22);
            return a* (X[1] - b * X[0] * X[0] + c * X[0] - d) *
                (X[1] - b * X[0] * X[0] + c * X[0] - d) +
                e * (1 - f) * Cos(X[0]) + e;
        }
    }

    class MartinGaddy : IFunction
    {
        public double BeginInterval { get { return 0; } }
        public double EndInterval { get { return 10; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            return (X[0] - X[1]) * (X[0] - X[1]) +
                ((X[0] + X[1] - 10) / 3.0) *
                ((X[0] + X[1] - 10) / 3.0);
        }
    }

    class Rosenbrock_1a : IFunction
    {
        public double BeginInterval { get { return -1.2; } }
        public double EndInterval { get { return 1.2; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            return 100 * (X[0] * X[0] - X[1]) *
                (X[0] * X[0] - X[1]) + (1 - X[0]) *
                (1 - X[0]);
        }
    }

    class Rosenbrock_1b : IFunction
    {
        public double BeginInterval { get { return -10; } }
        public double EndInterval { get { return 10; } }
        public int XQuant { get { return 2; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            return 100 * (X[0] * X[0] - X[1]) *
                (X[0] * X[0] - X[1]) + (1 - X[0]) *
                (1 - X[0]);
        }
    }

    class Rosenbrock_2 : IFunction
    {
        public double BeginInterval { get { return -1.2; } }
        public double EndInterval { get { return 1.2; } }
        public int XQuant { get { return 4; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            double Sum = 0;
            for (int i = 0; i < X.Length - 1; i++)
                Sum += 100 * (X[i] * X[i] - X[i+1]) *
                (X[i] * X[i] - X[i+1]) + (1 - X[i]) *
                (1 - X[i]);
            return Sum;
        }
    }

    class HyperSphere : IFunction
    {
        public double BeginInterval { get { return -5.12; } }
        public double EndInterval { get { return 5.12; } }
        public int XQuant { get { return 6; } }
        public bool Max { get { return false; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            double Sum = 0;
            for (int i = 0; i < X.Length; i++)
                Sum += X[i] * X[i];
            return Sum;
        }
    }

    class Griewangk : IFunction
    {
        public double BeginInterval { get { return -512; } }
        public double EndInterval { get { return 512; } }
        public int XQuant { get { return 10; } }
        public bool Max { get { return true; } }

        public double Function(double[] X)
        {
            if (XQuant != X.Length)
                return Double.NaN;
            double Sum = 0, Mult = 1;
            for(int i=0;i<X.Length;i++)
            {
                Sum += X[i] * X[i];
                Mult *= Cos(X[i] / Sqrt(i+1));     
            }
            return 1.0/(0.1 + Sum/400.0 - Mult + 1);
        }
    }

}
