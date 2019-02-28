using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public interface ITerm<T>
    {
        T GMapT<A>(MkT<A> lf);
    }

}
