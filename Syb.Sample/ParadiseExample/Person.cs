using System;

namespace Syb.Sample.ParadiseExample
{
    class Person : ITerm<Person>
    {
        public Person(Address address, Name name)
        {
            Name = name;
            Address = address;
        }
        public Address Address { get; private set; }
        public Name Name { get; private set; }

        public Person GMapT<A>(MkT<A> lf)
        {
            return new Person(lf.Apply(Address), lf.Apply(Name));
        }
    }
}