using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syb.Sample.ParadiseExample
{
    static class Functions
    {
        public static Company BuildCompany()
        {
            var blair = new Manager(new Salary(100000), new Person(new Address("London"), new Name("Blair")));
            var marlow = new Employee(new Salary(2000), new Person(new Address("Cambridge"), new Name("Marlow")));
            var joost = new Employee(new Salary(1000), new Person(new Address("Amsterdam"), new Name("Joost")));
            var ralf = new Manager(new Salary(2000), new Person(new Address("Amsterdam"), new Name("Ralf")));

            var strategy = new Dept(new Name("Strategy"), blair, new SubUnit[0]);
            var research = new Dept(new Name("Research"), ralf, new[] { new SubUnit(marlow), new SubUnit(joost) });

            return new Company(new[] { strategy, research });
        }

        /// <summary>
        /// Referring to the original SYB paper, this function is the actual code that is solving the problem
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        public static Salary Increase(decimal value, Salary salary) => new Salary(salary.Value * (1 + value));

        // All the functions below are considered boilerplate because we are traversing the company data structure
        public static Employee Increase(decimal value, Employee employee) => new Employee(Increase(value, employee.Salary), employee.Person);
        public static Manager Increase(decimal value, Manager employee) => new Manager(Increase(value, employee.Salary), employee.Person);
        public static Dept Increase(decimal value, Dept dept) => new Dept(dept.Name, Increase(value, dept.Manager), dept.Units.Select(p => Increase(value, p)).ToArray());
        public static SubUnit Increase(decimal value, SubUnit subUnit) => subUnit.Match(d => new SubUnit(Increase(value, d)), e => new SubUnit(Increase(value, e)));
        public static Company Increase(decimal value, Company company) => new Company(company.Departments.Select(p => Increase(value, p)).ToArray());
    }
}