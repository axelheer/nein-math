using System;
using System.Diagnostics;
using System.Numerics;

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
            Run<BigInteger, T>(valueSize, BigInteger, one);
            Run<Integer, U>(valueSize, Integer, two);
        }

        public static void Run<T, U>(
            int leftSize, int rightSize,
            Func<BigInteger, BigInteger, T> one,
            Func<Integer, Integer, U> two)
        {
            Run<BigInteger, T>(leftSize, rightSize, BigInteger, one);
            Run<Integer, U>(leftSize, rightSize, Integer, two);
        }

        public static void Run<T, U>(
            int leftSize, int rightSize, int otherSize,
            Func<BigInteger, BigInteger, BigInteger, T> one,
            Func<Integer, Integer, Integer, U> two)
        {
            Run<BigInteger, T>(leftSize, rightSize, otherSize, BigInteger, one);
            Run<Integer, U>(leftSize, rightSize, otherSize, Integer, two);
        }

        static void Run<T, U>(
            int valueSize,
            Func<int, T> create,
            Func<T, U> run)
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

        static void Run<T, U>(
            int leftSize, int rightSize,
            Func<int, T> create,
            Func<T, T, U> run)
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

        static void Run<T, U>(
            int leftSize, int rightSize, int otherSize,
            Func<int, T> create,
            Func<T, T, T, U> run)
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

        static Integer Integer(int size)
        {
            var value = new byte[(size + 7) / 8 + 1];
            random.NextBytes(value);
            value[value.Length - 1] = 0x00;
            return IntegerConverter.FromByteArray(value);
        }

        static BigInteger BigInteger(int size)
        {
            var value = new byte[(size + 7) / 8 + 1];
            random.NextBytes(value);
            value[value.Length - 1] = 0x00;
            return new BigInteger(value);
        }
    }
}
