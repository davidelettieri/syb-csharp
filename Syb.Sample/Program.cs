using Syb.Sample.ParadiseExample;
using System;
using System.Linq;

using static Syb.SybHelpers;
using static Syb.SybCombinators;
using Syb.Sample.v2;

namespace Syb.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParadiseExample();
            V2Example();
        }

        private static void V2Example()
        {
            var grandchild = new Grandchild("Paolo");

            var child = new Child(grandchild);
            var parent = new Parent(child);

            var lf = new LiftedFunction<Grandchild>(p => new Grandchild(p.Name + " Mais"));

            var result = Comb.Everywhere(lf, parent);
        }

        private static void ParadiseExample()
        {
            // Build example company
            var company = Functions.BuildCompany();

            // Lift function, mkT in the original paper
            var liftedIncrease = Lift<Salary>(p => Functions.Increase(0.1m, p));

            // Apply the lifted function everywhere
            var result = Everywhere(liftedIncrease, company) as Company;

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
