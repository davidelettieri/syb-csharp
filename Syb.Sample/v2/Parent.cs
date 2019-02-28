using System;
using System.Collections.Generic;
using System.Text;

namespace Syb.Sample.v2
{
    public class Parent : ITerm<Parent>
    {
        Child _child;

        public Parent(Child child)
        {
            _child = child;
        }

        public Parent GMapT<A>(MkT<A> lf)
        {
            return new Parent(lf.Apply(_child));
        }
    }
}
