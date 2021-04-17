using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class Pair<T1, T2>
    {
        public T1 Key;
        public T2 Value;

        public Pair(T1 key, T2 value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
