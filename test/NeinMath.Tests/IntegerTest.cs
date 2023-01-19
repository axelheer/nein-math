using System;
using Xunit;

namespace NeinMath.Tests
{
    public class IntegerTest
    {
        [Theory]
        [IntegerData]
        public void CastFromInt32(int value, Integer expected)
        {
            var actual = (Integer)value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void CastFromInt64(long value, Integer expected)
        {
            var actual = (Integer)value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void CastToInt32(Integer value, int expected)
        {
            using (Immutability.Guard(value))
            {
                var actual = (int)value;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public static void CastToInt32Overflow()
        {
            var tooBig = (Integer)int.MaxValue + 1;
            var tooSmall = (Integer)int.MinValue - 1;

            using (Immutability.Guard(tooBig, tooSmall))
            {
                Assert.Throws<OverflowException>(()
                    => (int)tooBig);
                Assert.Throws<OverflowException>(()
                    => (int)tooSmall);
            }
        }

        [Theory]
        [IntegerData]
        public void CastToInt64(Integer value, long expected)
        {
            using (Immutability.Guard(value))
            {
                var actual = (long)value;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public static void CastToInt64Overflow()
        {
            var tooBig = (Integer)long.MaxValue + 1;
            var tooSmall = (Integer)long.MinValue - 1;

            using (Immutability.Guard(tooBig, tooSmall))
            {
                Assert.Throws<OverflowException>(()
                    => (long)tooBig);
                Assert.Throws<OverflowException>(()
                    => (long)tooSmall);
            }
        }

        [Theory]
        [IntegerData]
        public void BitwiseAndInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left & right;
                var actualFu = left.BitwiseAnd(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void BitwiseAnd(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left & right;
                var actualFu = left.BitwiseAnd(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void BitwiseOrInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left | right;
                var actualFu = left.BitwiseOr(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void BitwiseOr(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left | right;
                var actualFu = left.BitwiseOr(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void XorInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left ^ right;
                var actualFu = left.Xor(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Xor(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left ^ right;
                var actualFu = left.Xor(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void OnesComplement(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = ~value;
                var actualFu = value.OnesComplement();

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void RightShift(Integer value, int shift, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = value >> shift;
                var actualFu = value.RightShift(shift);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void LeftShift(Integer value, int shift, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = value << shift;
                var actualFu = value.LeftShift(shift);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void EqualsInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left == right;
                var actualFu = left.Equals(right);
                var actualObj = left.Equals((object)right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
                Assert.Equal(expected, actualObj);
            }
        }

        [Theory]
        [IntegerData]
        public void EqualsBig(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left == right;
                var actualFu = left.Equals(right);
                var actualObj = left.Equals((object)right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
                Assert.Equal(expected, actualObj);
            }
        }

        [Fact]
        public static void EqualsNullOrInvalid()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.False(value.Equals(null));
                Assert.False(value.Equals(""));
            }
        }

        [Theory]
        [IntegerData]
        public void NotEqualsInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left != right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void NotEquals(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left != right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void CompareToInt(Integer left, int right, int expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left.CompareTo(right);
                var actualObj = left.CompareTo((object)right);

                Assert.Equal(expected, actual);
                Assert.Equal(expected, actualObj);
            }
        }

        [Theory]
        [IntegerData]
        public void CompareTo(Integer left, Integer right, int expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left.CompareTo(right);
                var actualObj = left.CompareTo((object)right);

                Assert.Equal(expected, actual);
                Assert.Equal(expected, actualObj);
            }
        }

        [Fact]
        public static void CompareToNullOrInvalid()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.Throws<ArgumentNullException>(()
                    => value.CompareTo(null));
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => value.CompareTo(""));
            }
        }

        [Theory]
        [IntegerData]
        public void LessThanInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left < right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void LessThan(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left < right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void LessThanOrEqualInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left <= right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void LessThanOrEqual(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left <= right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void GreaterThanInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left > right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void GreaterThan(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left > right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void GreaterThanOrEqualInt(Integer left, int right, bool expected)
        {
            using (Immutability.Guard(left))
            {
                var actual = left >= right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void GreaterThanOrEqual(Integer left, Integer right, bool expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actual = left >= right;

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void Plus(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = +value;
                var actualFu = value.Plus();

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Increment(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = value; ++actualOp;
                var actualFu = value.Increment();

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void AddInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left + right;
                var actualFu = left.Add(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Add(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left + right;
                var actualFu = left.Add(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Negate(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = -value;
                var actualFu = value.Negate();

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Decrement(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = value; --actualOp;
                var actualFu = value.Decrement();

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void SubtractInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left - right;
                var actualFu = left.Subtract(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Subtract(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left - right;
                var actualFu = left.Subtract(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Square(Integer value, Integer expected)
        {
            using (Immutability.Guard(value))
            {
                var actualOp = value * value;
                var actualFu = value.Multiply(value);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void MultiplyInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left * right;
                var actualFu = left.Multiply(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void Multiply(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left * right;
                var actualFu = left.Multiply(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Theory]
        [IntegerData]
        public void DivideInt(Integer left, int right, Integer expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left / right;
                var actualFu = left.Divide(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Fact]
        public static void DivideIntZero()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.Throws<DivideByZeroException>(()
                    => value / 0);
            }
        }

        [Theory]
        [IntegerData]
        public void Divide(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left / right;
                var actualFu = left.Divide(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Fact]
        public static void DivideZero()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.Throws<DivideByZeroException>(()
                    => value / value);
            }
        }

        [Theory]
        [IntegerData]
        public void RemainderInt(Integer left, int right, int expected)
        {
            using (Immutability.Guard(left))
            {
                var actualOp = left % right;
                var actualFu = left.Remainder(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Fact]
        public static void RemainderIntZero()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.Throws<DivideByZeroException>(()
                    => value % 0);
            }
        }

        [Theory]
        [IntegerData]
        public void Remainder(Integer left, Integer right, Integer expected)
        {
            using (Immutability.Guard(left, right))
            {
                var actualOp = left % right;
                var actualFu = left.Remainder(right);

                Assert.Equal(expected, actualOp);
                Assert.Equal(expected, actualFu);
            }
        }

        [Fact]
        public static void RemainderZero()
        {
            var value = (Integer)0;

            using (Immutability.Guard(value))
            {
                Assert.Throws<DivideByZeroException>(()
                    => value % value);
            }
        }

        [Theory]
        [IntegerData]
        public void String(Integer value, string expected)
        {
            using (Immutability.Guard(value))
            {
                var actual = value.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [IntegerData]
        public void Parse(string value, Integer expected)
        {
            var actual = Integer.Parse(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void HashCodeInt(Integer value, int expected)
        {
            using (Immutability.Guard(value))
            {
                Assert.Equal(expected, value.GetHashCode());
            }
        }

        [Theory]
        [IntegerData]
        public void HashCode(Integer value)
        {
            using (Immutability.Guard(value))
            {
                var expected = value.GetHashCode();

                Assert.Equal(expected, value.GetHashCode());
                Assert.NotEqual(expected, (value + 1).GetHashCode());
                Assert.NotEqual(expected, (value - 1).GetHashCode());
                if (value != 0)
                {
                    Assert.NotEqual(expected, (-value).GetHashCode());
                    Assert.NotEqual(expected, (value + value).GetHashCode());
                    Assert.NotEqual(expected, (value - value).GetHashCode());
                    if (value != 1)
                    {
                        Assert.NotEqual(expected, (value * value).GetHashCode());
                        Assert.NotEqual(expected, (value / value).GetHashCode());
                    }
                }
            }
        }
    }
}
