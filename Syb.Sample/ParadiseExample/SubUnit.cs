using System;

namespace Syb.Sample.ParadiseExample
{
    class SubUnit : ITerm
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

        public ITerm GMapT<U>(Func<ITerm, U> f) where U : ITerm
        {
            Func<Dept, ITerm> fd = d => new SubUnit(f(d) as Dept);
            Func<Employee, ITerm> fe = e => new SubUnit(f(e) as Employee);

            return Match(fd, fe);
        }

    }
}