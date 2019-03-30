using System;
using System.Linq;

namespace Syb.Sample.ParadiseExample
{
    class Dept : ITerm<Dept>
    {
        public Dept(Name name, Manager manager, SubUnit[] units)
        {
            Name = name;
            Manager = manager;
            Units = units;
        }
        public Name Name { get; private set; }
        public Manager Manager { get; private set; }
        public SubUnit[] Units { get; private set; }

        public Dept GMapT<A>(MkT<A> lf)
        {
            return new Dept(lf.Apply(Name), lf.Apply(Manager), Units.Select(u => lf.Apply(u)).ToArray());
        }
    }
}