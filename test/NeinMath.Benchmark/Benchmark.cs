using System;
using System.Diagnostics;
using System.Numerics;

#pragma warning disable CA1303
#pragma warning disable CA1715
#pragma warning disable CA1724
#pragma warning disable CA2211

namespace NeinMath.Benchmark
{
    public static class Benchmark
    {
        public static int RunCount = 10;
        public static int ValCount = 100;

        static readonly Random random = new Random(1138);

        public static void Run<T, U>(
            int valueSize,
            Func<BigInteger, T> one,
            Func<Integer, U> two)
        {
            var oneValues = BigIntegers(valueSize);
            Run("Numerics", i => one(oneValues[i]));

            var twoValues = Integers(valueSize);
            Run("NeinMath", i => two(twoValues[i]));
        }

        public static void Run<T, U>(
            int leftSize, int rightSize,
            Func<BigInteger, BigInteger, T> one,
            Func<Integer, Integer, U> two)
        {
            var oneLeft = BigIntegers(leftSize);
            var oneRight = BigIntegers(rightSize);
            Run("Numerics", i => one(oneLeft[i], oneRight[i]));

            var twoLeft = Integers(leftSize);
            var twoRight = Integers(rightSize);
            Run("NeinMath", i => two(twoLeft[i], twoRight[i]));
        }

        public static void Run<T, U>(
            int leftSize, int rightSize, int otherSize,
            Func<BigInteger, BigInteger, BigInteger, T> one,
            Func<Integer, Integer, Integer, U> two)
        {
            var oneLeft = BigIntegers(leftSize);
            var oneRight = BigIntegers(rightSize);
            var oneOther = BigIntegers(otherSize);
            Run("Numerics", i => one(oneLeft[i], oneRight[i], oneOther[i]));

            var twoLeft = Integers(leftSize);
            var twoRight = Integers(rightSize);
            var twoOther = Integers(otherSize);
            Run("NeinMath", i => two(twoLeft[i], twoRight[i], twoOther[i]));
        }

        static void Run(string name, Action<int> operation)
        {
            var watch = new Stopwatch();
            var result = long.MaxValue;

            operation(0);

            Console.Write("{0}...", name);

            for (var j = 0; j < RunCount; j++)
            {
                watch.Restart();
                for (var i = 0; i < ValCount; i++)
                    operation(i);
                watch.Stop();

                result = Math.Min(result, watch.ElapsedMilliseconds);
            }

            // tada!
            Console.WriteLine("{0} ms / {1} ops", result, ValCount);
        }

        static Integer[] Integers(int size)
        {
            var values = new Integer[ValCount];
            for (var i = 0; i < ValCount; i++)
                values[i] = IntegerConverter.FromByteArray(RandomBytes(size));
            return values;
        }

        static BigInteger[] BigIntegers(int size)
        {
            var values = new BigInteger[ValCount];
            for (var i = 0; i < ValCount; i++)
                values[i] = new BigInteger(RandomBytes(size));
            return values;
        }

        static byte[] RandomBytes(int size)
        {
            var value = new byte[(size + 8) / 8];
            while (IsZero(value))
            {
                random.NextBytes(value);
                // ensure actual bit count (remaining bits not set)
                // ensure positive value (highest-order bit not set)
                value[value.Length - 1] &= (byte)(0xFF >> 8 - size % 8);
            }
            return value;
        }

        static bool IsZero(byte[] value)
        {
            foreach (var b in value)
            {
                if (b != 0)
                    return false;
            }
            return true;
        }
    }
}
