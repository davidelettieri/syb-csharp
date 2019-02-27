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

    public static class Comb
    {
        public static T Everywhere<T, U>(LiftedFunction<U> f, T t)
            where T : ITerm<T>
            where U : ITerm<U>
        {
            return f.Apply(t.GMapT(new EveryWhere<U>(f)));
        }
    }

    public interface ITerm<T>
    {
        T GMapT<A>(LiftedFunction<A> lf);
    }

    public class A : ITerm<A>
    {
        public A GMapT<A1>(LiftedFunction<A1> lf)
        {
            return new A();
        }
    }

    public class B : ITerm<B>
    {
        A _a;
        public B(A a)
        {
            _a = a;
        }

        public B GMapT<T>(LiftedFunction<T> lf)
        {
            return new B(lf.Apply(_a));
        }
    }

    public class EveryWhere<U> : LiftedFunction<U>
    {
        public EveryWhere(LiftedFunction<U> f) : base(f.F)
        {
        }

        public override T Apply<T>(T t)
        {
            return base.Apply(t.GMapT(this));
        }
    }

    public class LiftedFunction<A>
    {
        protected Func<A, A> _f;
        public Func<A, A> F => _f;

        public LiftedFunction(Func<A, A> f)
        {
            _f = f;
        }

        public virtual T Apply<T>(T t)
            where T : ITerm<T>
        {
            if (_f is Func<T, T> ft)
            {
                return ft(t);
            }

            return t;
        }
    }
}
