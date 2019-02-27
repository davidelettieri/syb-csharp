using System;

namespace Syb.Sample.ParadiseExample
{
    class Salary : ITerm
    {
        public Salary(decimal value)
        {
            Value = value;
        }
        public decimal Value { get; private set; }

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Salary(Value);
        }
    }
}