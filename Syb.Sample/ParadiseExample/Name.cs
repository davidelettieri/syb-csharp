using System;

namespace Syb.Sample.ParadiseExample
{
    class Name : ITerm
    {
        public Name(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Name(Value);
        }
    }
}