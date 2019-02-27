using System;

namespace Syb.Sample.ParadiseExample
{
    class Person : ITerm
    {
        public Person(Address address, Name name)
        {
            Name = name;
            Address = address;
        }
        public Address Address { get; private set; }
        public Name Name { get; private set; }

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Person(f(Address) as Address, f(Name) as Name);
        }
    }
}