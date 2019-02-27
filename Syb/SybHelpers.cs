using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public static class SybHelpers
    {
        public static Func<ITerm, ITerm> Lift<T>(Func<T, T> f) where T : ITerm
        {
            return p => (p is T t) ? f(t) : p;
        }
    }
}
