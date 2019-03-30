using System;
using System.Collections.Generic;
using System.Text;

namespace Syb.Sample.v2
{
    public class Grandchild : ITerm<Grandchild>
    {
        public string Name { get; }

        public Grandchild(string name)
        {
            Name = name;
        }

        public Grandchild GMapT<A>(MkT<A> lf)
        {
            return new Grandchild(Name);
        }
    }
}
