using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public interface IFunction
    {
        double BeginInterval { get; }
        double EndInterval { get; }
        int XQuant { get; }
        bool Max { get; }

        double Function(double [] X);

    }
}
