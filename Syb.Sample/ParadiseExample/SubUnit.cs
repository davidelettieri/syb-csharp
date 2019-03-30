using System;

namespace Syb.Sample.ParadiseExample
{
    class SubUnit : ITerm<SubUnit>
    {
        readonly Employee _employee;
        readonly Dept _dept;
        readonly int _type;

        public SubUnit(Employee employee)
        {
            _employee = employee;
            _type = 0;
        }

        public SubUnit(Dept dept)
        {
            _dept = dept;
            _type = 1;
        }

        public T Match<T>(Func<Dept, T> f, Func<Employee, T> g)
        {
            switch (_type)
            {
                case 0:
                    return g(_employee);
                case 1:
                    return f(_dept);
                default:
                    throw new NotImplementedException();
            }
        }

        public SubUnit GMapT<A>(MkT<A> lf)
        {
            Func<Dept, SubUnit> fd = d => new SubUnit(lf.Apply(d));
            Func<Employee, SubUnit> fe = e => new SubUnit(lf.Apply(e));

            return Match(fd, fe);
        }
    }
}