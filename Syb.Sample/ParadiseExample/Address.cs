using System;

namespace Syb.Sample.ParadiseExample
{
    class Address : ITerm<Address>
    {
        public Address(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public Address GMapT<A>(MkT<A> lf)
        {
            return new Address(Value);
        }
    }
}