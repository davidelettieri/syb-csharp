using System;

namespace Syb.Sample.ParadiseExample
{
    class Salary : ITerm<Salary>
    {
        public Salary(decimal value)
        {
            Value = value;
        }
        public decimal Value { get; private set; }

        public Salary GMapT<A>(MkT<A> lf)
        {
            return this;
        }
    }
}