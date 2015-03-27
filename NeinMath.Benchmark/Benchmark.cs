using System;
using System.Diagnostics;
using System.Numerics;

namespace NeinMath.Benchmark
{
    public static class Benchmark
    {
        private const int RunCount = 10;
        private const int ValCount = 100;

        private static readonly Random random = new Random(1138);

        public static void Run(
            int valueSize,
            Func<BigInteger, BigInteger> one,
            Func<Integer, Integer> two)
        {
            Run<BigInteger>(valueSize, BigInteger, one);
            Run<Integer>(valueSize, Integer, two);
        }

        public static void Run(
            int leftSize, int rightSize,
            Func<BigInteger, BigInteger, BigInteger> one,
            Func<Integer, Integer, Integer> two)
        {
            Run<BigInteger>(leftSize, rightSize, BigInteger, one);
            Run<Integer>(leftSize, rightSize, Integer, two);
        }

        public static void Run(
            int leftSize, int rightSize, int otherSize,
            Func<BigInteger, BigInteger, BigInteger, BigInteger> one,
            Func<Integer, Integer, Integer, Integer> two)
        {
            Run<BigInteger>(leftSize, rightSize, otherSize, BigInteger, one);
            Run<Integer>(leftSize, rightSize, otherSize, Integer, two);
        }

        private static void Run<T>(
            int valueSize,
            Func<int, T> create,
            Func<T, T> run)
        {
            var value = new T[ValCount];

            // seed test values
            for (var i = 0; i < ValCount; i++)
            {
                value[i] = create(valueSize);
            }

            Console.Write("{0}: ", typeof(T).Name);

            // run benchmarks
            var result = long.MaxValue;
            for (var j = 0; j < RunCount; j++)
            {
                var watch = new Stopwatch();
                watch.Start();
                for (var i = 0; i < ValCount; i++)
                    run(value[i]);
                watch.Stop();

                if (watch.ElapsedMilliseconds < result)
                    result = watch.ElapsedMilliseconds;
            }

            // tada!
            Console.WriteLine("{0} ms / {1} ops", result, ValCount);
        }

        private static void Run<T>(
            int leftSize, int rightSize,
            Func<int, T> create,
            Func<T, T, T> run)
        {
            var left = new T[ValCount];
            var right = new T[ValCount];

            // seed test values
            for (var i = 0; i < ValCount; i++)
            {
                left[i] = create(leftSize);
                right[i] = create(rightSize);
            }

            Console.Write("{0}: ", typeof(T).Name);

            // run benchmarks
            var result = long.MaxValue;
            for (var j = 0; j < RunCount; j++)
            {
                var watch = new Stopwatch();
                watch.Start();
                for (var i = 0; i < ValCount; i++)
                    run(left[i], right[i]);
                watch.Stop();

                if (watch.ElapsedMilliseconds < result)
                    result = watch.ElapsedMilliseconds;
            }

            // tada!
            Console.WriteLine("{0} ms / {1} ops", result, ValCount);
        }

        private static void Run<T>(
            int leftSize, int rightSize, int otherSize,
            Func<int, T> create,
            Func<T, T, T, T> run)
        {
            var left = new T[ValCount];
            var right = new T[ValCount];
            var other = new T[ValCount];

            // seed test values
            for (var i = 0; i < ValCount; i++)
            {
                left[i] = create(leftSize);
                right[i] = create(rightSize);
                other[i] = create(otherSize);
            }

            Console.Write("{0}: ", typeof(T).Name);

            // run benchmarks
            var result = long.MaxValue;
            for (var j = 0; j < RunCount; j++)
            {
                var watch = new Stopwatch();
                watch.Start();
                for (var i = 0; i < ValCount; i++)
                    run(left[i], right[i], other[i]);
                watch.Stop();

                if (watch.ElapsedMilliseconds < result)
                    result = watch.ElapsedMilliseconds;
            }

            // tada!
            Console.WriteLine("{0} ms / {1} ops", result, ValCount);
        }

        private static Integer Integer(int size)
        {
            var value = new byte[(size + 7) / 8 + 1];
            random.NextBytes(value);
            value[value.Length - 1] = 0x00;
            return IntegerConverter.FromByteArray(value);
        }

        private static BigInteger BigInteger(int size)
        {
            var value = new byte[(size + 7) / 8 + 1];
            random.NextBytes(value);
            value[value.Length - 1] = 0x00;
            return new BigInteger(value);
        }
    }
}
