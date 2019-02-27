using System;

namespace Syb.Sample.ParadiseExample
{
    class Employee : ITerm
    {
        public Employee(Salary salary, Person person)
        {
            Salary = salary;
            Person = person;
        }
        public Salary Salary { get; private set; }
        public Person Person { get; private set; }

        public virtual ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Employee(f(Salary) as Salary, f(Person) as Person);
        }
    }
}