using System;
using System.Linq;

namespace Syb.Sample.ParadiseExample
{
    class Company : ITerm
    {
        public Company(Dept[] departments)
        {
            Departments = departments;
        }
        public Dept[] Departments { get; private set; }

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Company(Departments.Select(d => f(d) as Dept).ToArray());
        }
    }
}