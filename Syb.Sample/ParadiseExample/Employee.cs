using System;

namespace Syb.Sample.ParadiseExample
{
    class Employee : ITerm<Employee>
    {
        public Employee(Salary salary, Person person)
        {
            Salary = salary;
            Person = person;
        }
        public Salary Salary { get; private set; }
        public Person Person { get; private set; }

        public Employee GMapT<A>(MkT<A> lf)
        {
            return new Employee(lf.Apply(Salary), lf.Apply(Person));
        }
    }
}