using System;
using System.Linq;

namespace Syb.Sample.ParadiseExample
{
    class Dept : ITerm
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

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            return new Dept(f(Name) as Name, f(Manager) as Manager, Units.Select(u => f(u) as SubUnit).ToArray());
        }
    }
}