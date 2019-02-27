using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public interface ITerm
    {
        ITerm GMapT<U>(Func<ITerm, U> f)
            where U : ITerm;
    }
}
