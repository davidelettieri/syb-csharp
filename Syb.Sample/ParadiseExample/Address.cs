using System;

namespace Syb.Sample.ParadiseExample
{
    class Address : ITerm
    {
        public Address(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Address(Value);
        }
    }
}