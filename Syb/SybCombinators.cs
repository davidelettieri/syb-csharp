using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public static class SybCombinators
    {
        public static ITerm Everywhere<T>(Func<ITerm, ITerm> f, T t) where T : ITerm
        {
            return f(t.GMapT(u => Everywhere(f, u)));
        }
    }
}
