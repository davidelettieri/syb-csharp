using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public static class Combinators
    {
        private class EveryWhere<U> : MkT<U>
        {
            public EveryWhere(MkT<U> f) : base(f.Function)
            {
            }

            public override T Apply<T>(T t)
            {
                return base.Apply(t.GMapT(this));
            }
        }

        public static T Everywhere<T, U>(MkT<U> f, T t)
           where T : ITerm<T>
           where U : ITerm<U>
        {
            return f.Apply(t.GMapT(new EveryWhere<U>(f)));
        }
    }
}
