﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Syb
{
    public class MkT<A>
    {
        protected Func<A, A> _function;
        internal Func<A, A> Function => _function;

        public MkT(Func<A, A> f)
        {
            _function = f;
        }

        public virtual T Apply<T>(T t)
            where T : ITerm<T>
        {
            if (_function is Func<T, T> ft)
            {
                return ft(t);
            }

            return t;
        }
    }
}
