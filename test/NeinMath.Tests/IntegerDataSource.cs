using System;
using System.Numerics;

namespace NeinMath.Tests
{
    public abstract class IntegerDataSource
    {
        const int testCount = 569;

        // don't make it too random...
        readonly Random random = new Random(1138);

        protected double[] Floats()
        {
            return Floats(_ => true, x => x);
        }

        protected double[] Floats(Func<double, bool> predicate)
        {
            return Floats(predicate, x => x);
        }

        protected double[] Floats(Func<double, double> selector)
        {
            return Floats(_ => true, selector);
        }

        protected double[] Floats(Func<double, bool> predicate,
                                  Func<double, double> selector)
        {
            // mix some common values...
            // ...with something random!
            var result = new double[testCount];
            for (var i = 0; i < testCount; i++)
            {
                var value = default(double);
                do
                {
                    switch (random.Next() % 8)
                    {
                        case 0:
                            value = random.Next(-3, 4);
                            break;

                        case 1:
                            value = new double[]
                            {
                                double.PositiveInfinity,
                                double.NegativeInfinity,
                                double.MinValue,
                                double.MaxValue,
                                double.NaN
                            }[random.Next(5)];
                            break;

                        default:
                            value = random.NextDouble();
                            break;
                    }
                }
                while (!predicate(value));
                result[i] = selector(value);
            }
            return result;
        }

        protected int[] Integers()
        {
            return Integers(_ => true, x => x);
        }

        protected int[] Integers(Func<int, bool> predicate)
        {
            return Integers(predicate, x => x);
        }

        protected int[] Integers(Func<int, int> selector)
        {
            return Integers(_ => true, selector);
        }

        protected int[] Integers(Func<int, bool> predicate,
                                 Func<int, int> selector)
        {
            // mix some common values...
            // ...with something random!
            var result = new int[testCount];
            for (var i = 0; i < testCount; i++)
            {
                var value = default(int);
                do
                {
                    switch (random.Next() % 8)
                    {
                        case 0:
                            value = random.Next(-3, 4);
                            break;

                        case 1:
                            value = new int[]
                            {
                                int.MinValue,
                                int.MaxValue
                            }[random.Next(2)];
                            break;

                        default:
                            value = random.Next();
                            break;
                    }
                }
                while (!predicate(value));
                result[i] = selector(value);
            }
            return result;
        }

        protected long[] LongIntegers()
        {
            return LongIntegers(_ => true, x => x);
        }

        protected long[] LongIntegers(Func<long, bool> predicate)
        {
            return LongIntegers(predicate, x => x);
        }

        protected long[] LongIntegers(Func<long, long> selector)
        {
            return LongIntegers(_ => true, selector);
        }

        protected long[] LongIntegers(Func<long, bool> predicate,
                                      Func<long, long> selector)
        {
            // mix some common values...
            // ...with something random!
            var result = new long[testCount];
            for (var i = 0; i < testCount; i++)
            {
                var value = default(long);
                do
                {
                    switch (random.Next() % 8)
                    {
                        case 0:
                            value = random.Next(-3, 4);
                            break;

                        case 1:
                            value = new long[]
                            {
                                long.MinValue,
                                long.MaxValue
                            }[random.Next(2)];
                            break;

                        default:
                            value = (long)random.Next()
                                  | (long)random.Next() << 32;
                            break;
                    }
                }
                while (!predicate(value));
                result[i] = selector(value);
            }
            return result;
        }

        protected BigInteger[] BigIntegers()
        {
            return BigIntegers(_ => true, x => x);
        }

        protected BigInteger[] BigIntegers(Func<BigInteger, bool> predicate)
        {
            return BigIntegers(predicate, x => x);
        }

        protected BigInteger[] BigIntegers(Func<BigInteger, BigInteger> selector)
        {
            return BigIntegers(_ => true, selector);
        }

        protected BigInteger[] BigIntegers(Func<BigInteger, bool> predicate,
                                           Func<BigInteger, BigInteger> selector)
        {
            // mix some common values...
            // ...with something random!
            var result = new BigInteger[testCount];
            for (var i = 0; i < testCount; i++)
            {
                var value = default(BigInteger);
                do
                {
                    var length = default(int);
                    var bytes = default(byte[]);

                    switch (random.Next() % 8)
                    {
                        case 0:
                            value = random.Next(-3, 4);
                            break;

                        case 1:
                            length = random.Next(1, 100);
                            bytes = new byte[length];
                            for (var j = 0; j < bytes.Length - 1; j++)
                                bytes[j] = 0xFF;
                            value = new BigInteger(bytes);
                            break;

                        case 2:
                            length = random.Next(1, 100);
                            bytes = new byte[length];
                            bytes[bytes.Length - 1] = 1;
                            value = new BigInteger(bytes);
                            break;

                        default:
                            length = random.Next(1, 100);
                            bytes = new byte[length];
                            random.NextBytes(bytes);
                            value = new BigInteger(bytes);
                            break;
                    }
                }
                while (!predicate(value));
                result[i] = selector(value);
            }
            return result;
        }

        protected Tuple<T, U>[] Items<T, U>(T[] left, U[] right)
        {
            var result = new Tuple<T, U>[testCount];
            for (var i = 0; i < testCount; i++)
                result[i] = Tuple.Create(left[i], right[i]);
            return result;
        }

        protected Tuple<T, U, V>[] Items<T, U, V>(T[] left, U[] right, V[] other)
        {
            var result = new Tuple<T, U, V>[testCount];
            for (var i = 0; i < testCount; i++)
                result[i] = Tuple.Create(left[i], right[i], other[i]);
            return result;
        }

        protected Integer ToInteger(BigInteger value)
        {
            return IntegerConverter.FromByteArray(value.ToByteArray());
        }
    }
}
