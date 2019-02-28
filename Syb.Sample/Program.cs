using Syb.Sample.ParadiseExample;
using System;
using System.Linq;
using static Syb.Combinators;
using Syb.Sample.v2;

namespace Syb.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            ParadiseExample();
            V2Example();
        }

        private static void V2Example()
        {
            var grandchild = new Grandchild("Mario");

            var child = new Child(grandchild);
            var parent = new Parent(child);

            var lf = new MkT<Grandchild>(p => new Grandchild(p.Name + " Rossi"));

            var result = Everywhere(lf, parent);
        }

        private static void ParadiseExample()
        {
            // Build example company
            var company = Functions.BuildCompany();

            // Lift function, mkT in the original paper
            var liftedIncrease = new MkT<Salary>(p => Functions.Increase(0.1m, p));

            // Apply the lifted function everywhere
            var result = Everywhere(liftedIncrease, company);

            // Salary is increased, check only one to verify it
            var original = company.Departments
                                  .FirstOrDefault(p => p.Name.Value == "Strategy")
                                  .Manager.Salary.Value;

            var final = result.Departments
                              .FirstOrDefault(p => p.Name.Value == "Strategy")
                              .Manager.Salary.Value;

            Console.WriteLine($"Starting salary is {original}, final salary is {final}");
        }
    }
}
