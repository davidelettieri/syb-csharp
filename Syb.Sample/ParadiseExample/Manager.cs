using System;

namespace Syb.Sample.ParadiseExample
{
    class Manager : Employee, ITerm
    {
        public Manager(Salary salary, Person person) : base(salary, person)
        {

        }

        public override ITerm GMapT<U>(Func<ITerm, U> f)
        {
            return new Manager(f(Salary) as Salary, f(Person) as Person);
        }
    }
}