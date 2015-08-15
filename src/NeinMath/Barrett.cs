namespace NeinMath
{
    internal sealed class Barrett
    {
        private readonly uint[] m;
        private readonly int ml;

        private readonly uint[] u;
        private readonly int ul;

        private readonly int L;

        private Barrett(Integer modulus, Integer R, Integer mu)
        {
            m = modulus.bits;
            ml = modulus.length;

            u = mu.bits;
            ul = mu.length;

            L = R.length;
        }

        public static Barrett Begin(Integer modulus)
        {
            var bits = new uint[modulus.length * 2 + 1];
            bits[bits.Length - 1] = 1;
            var R = new Integer(bits, bits.Length, false);

            return new Barrett(modulus, R, R / modulus);
        }

        public Integer Reduce(Integer value)
        {
            var v = value.bits;
            var vl = value.length;

            if (vl >= L)
            {
                Calc.Divide(v, vl, m, ml, out v);
                return new Integer(v, v.Length, value.sign);
            }

            var l1 = vl - ml + 1;
            var q1 = l1 > 0
                ? Calc.Multiply(v, l1, ml - 1, u, ul, 0)
                : new uint[] { };

            var l2 = Bits.Length(q1, q1.Length) - ml - 1;
            var q2 = l2 > 0
                ? Calc.Multiply(q1, l2, ml + 1, m, ml, 0)
                : new uint[] { };

            var q = q2;
            var ql = Bits.Length(q, q.Length);

            var r = Calc.Subtract(v, vl > ml + 1 ? ml + 1 : vl,
                                  q, ql > ml + 1 ? ml + 1 : ql);
            var rl = Bits.Length(r, r.Length);

            while (Bits.Compare(r, rl, m, ml) >= 0)
            {
                Calc.SubtractSelf(r, rl, m, ml);
                rl = Bits.Length(r, r.Length);
            }

            return new Integer(r, rl, value.sign);
        }
    }
}
