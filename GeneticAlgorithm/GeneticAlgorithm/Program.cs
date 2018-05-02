using System;
using System.Linq;
using System.Reflection;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly a = Assembly.LoadFrom("GeneticAlgorithm.exe");
            var types =
                from t in a.GetTypes()
                where t.GetInterface("IFunction") != null
                let name = t.Name
                select new
                {
                    Name = name,
                    Instance = t
                };
            foreach (var elem in types)
            {
                //if (elem.Instance.Name == "GoldsteinPrice")            //Choiсe of formula
                //{
                    Console.WriteLine($"\n\n----{elem.Name}:");
                    GenAlgorithm test = new GenAlgorithm((IFunction)
                        Activator.CreateInstance(elem.Instance),
                        2048, 100);
                    test.Result();
                //}
            }
        }
    }
}
