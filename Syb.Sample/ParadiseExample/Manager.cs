using System;

namespace Syb.Sample.ParadiseExample
{
    class Manager : Employee, ITerm<Manager>
    {
        public Manager(Salary salary, Person person) : base(salary, person)
        {

        }

        public new Manager GMapT<A>(MkT<A> lf)
        {
            return new Manager(lf.Apply(Salary), lf.Apply(Person));
        }
    }
}