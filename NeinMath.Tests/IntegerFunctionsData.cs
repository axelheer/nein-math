using System;
using System.Numerics;
using Xunit;

namespace NeinMath.Tests
{
    public class IntegerFunctionsData : IntegerDataSource
    {
        public TheoryData<Integer, int> Sgn()
        {
            var data = new TheoryData<Integer, int>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), i.Sign);
            return data;
        }

        public TheoryData<Integer, Integer> Abs()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(BigInteger.Abs(i)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Min()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(BigInteger.Min(i.Item1, i.Item2)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Max()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(BigInteger.Max(i.Item1, i.Item2)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Gcd()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(BigInteger.GreatestCommonDivisor(i.Item1, i.Item2)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Lcm()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(BigInteger.Abs(i.Item1 * i.Item2)
                    / BigInteger.GreatestCommonDivisor(i.Item1, i.Item2)));
            return data;
        }

        public TheoryData<Integer, Integer> Inv()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers(x => x != 0,
                                                               x => BigInteger.Abs(x))))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2));
            return data;
        }

        public TheoryData<Integer, int, Integer> Pow()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers(x => Math.Abs(x % 10))))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(BigInteger.Pow(i.Item1, i.Item2)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer, Integer> ModPow()
        {
            var data = new TheoryData<Integer, Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(),
                                    BigIntegers(x => BigInteger.Abs(x)),
                                    BigIntegers(x => x != 0, x => BigInteger.Abs(x))))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2), ToInteger(i.Item3),
                    ToInteger(BigInteger.ModPow(i.Item1, i.Item2, i.Item3)));
            return data;
        }

        public TheoryData<Integer, Integer, Integer, Integer> DivRem()
        {
            var data = new TheoryData<Integer, Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers(x => x != 0)))
            {
                var remainder = default(BigInteger);
                var expected = BigInteger.DivRem(i.Item1, i.Item2, out remainder);
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2), ToInteger(expected),
                    ToInteger(remainder));
            }
            return data;
        }
    }
}
