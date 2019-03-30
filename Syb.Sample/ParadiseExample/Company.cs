using System;
using System.Linq;

namespace Syb.Sample.ParadiseExample
{
    class Company : ITerm<Company>
    {
        public Company(Dept[] departments)
        {
            Departments = departments;
        }
        public Dept[] Departments { get; private set; }

        public Company GMapT<A>(MkT<A> lf)
        {
            return new Company(Departments.Select(d => lf.Apply(d)).ToArray());
        }
    }
}