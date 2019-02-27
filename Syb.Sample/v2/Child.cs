using System;
using System.Collections.Generic;
using System.Text;

namespace Syb.Sample.v2
{
    public class Child : ITerm<Child>
    {
        Grandchild _grandchild;

        public Child(Grandchild grandchild)
        {
            _grandchild = grandchild;
        }

        public Child GMapT<A>(LiftedFunction<A> lf)
        {
            return new Child(lf.Apply(_grandchild));
        }
    }
}
