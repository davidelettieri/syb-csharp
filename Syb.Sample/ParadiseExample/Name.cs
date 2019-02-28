using System;

namespace Syb.Sample.ParadiseExample
{
    class Name : ITerm<Name>
    {
        public Name(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public Name GMapT<A>(MkT<A> lf)
        {
            return this;
        }
    }
}