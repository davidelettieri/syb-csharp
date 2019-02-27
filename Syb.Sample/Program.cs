using Syb.Sample.ParadiseExample;
using System;
using System.Linq;

using static Syb.SybHelpers;
using static Syb.SybCombinators;

namespace Syb.Sample
{
    class Program
    {
        static void Main(string[] args)
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
