using System;
using Xunit;

namespace NeinMath.Tests
{
    public class IntegerTest
    {
        [Theory]
        [IntegerData]
        public void BitwiseAndInt(Integer left, int right, Integer expected)
        {
            var actualOp = left & right;
            var actualFu = left.BitwiseAnd(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void BitwiseAnd(Integer left, Integer right, Integer expected)
        {
            var actualOp = left & right;
            var actualFu = left.BitwiseAnd(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void BitwiseOrInt(Integer left, int right, Integer expected)
        {
            var actualOp = left | right;
            var actualFu = left.BitwiseOr(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void BitwiseOr(Integer left, Integer right, Integer expected)
        {
            var actualOp = left | right;
            var actualFu = left.BitwiseOr(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void XorInt(Integer left, int right, Integer expected)
        {
            var actualOp = left ^ right;
            var actualFu = left.Xor(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Xor(Integer left, Integer right, Integer expected)
        {
            var actualOp = left ^ right;
            var actualFu = left.Xor(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void OnesComplement(Integer value, Integer expected)
        {
            var actualOp = ~value;
            var actualFu = value.OnesComplement();

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void RightShift(Integer value, int shift, Integer expected)
        {
            var actualOp = value >> shift;
            var actualFu = value.RightShift(shift);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void LeftShift(Integer value, int shift, Integer expected)
        {
            var actualOp = value << shift;
            var actualFu = value.LeftShift(shift);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void EqualsInt(Integer left, int right, bool expected)
        {
            var actualOp = left == right;
            var actualFu = left.Equals(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Equals(Integer left, Integer right, bool expected)
        {
            var actualOp = left == right;
            var actualFu = left.Equals(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void NotEqualsInt(Integer left, int right, bool expected)
        {
            var actual = left != right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void NotEquals(Integer left, Integer right, bool expected)
        {
            var actual = left != right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void CompareToInt(Integer left, int right, int expected)
        {
            var actual = left.CompareTo(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void CompareTo(Integer left, Integer right, int expected)
        {
            var actual = left.CompareTo(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void LessThanInt(Integer left, int right, bool expected)
        {
            var actual = left < right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void LessThan(Integer left, Integer right, bool expected)
        {
            var actual = left < right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void LessThanOrEqualInt(Integer left, int right, bool expected)
        {
            var actual = left <= right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void LessThanOrEqual(Integer left, Integer right, bool expected)
        {
            var actual = left <= right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void GreaterThanInt(Integer left, int right, bool expected)
        {
            var actual = left > right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void GreaterThan(Integer left, Integer right, bool expected)
        {
            var actual = left > right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void GreaterThanOrEqualInt(Integer left, int right, bool expected)
        {
            var actual = left >= right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void GreaterThanOrEqual(Integer left, Integer right, bool expected)
        {
            var actual = left >= right;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Plus(Integer value, Integer expected)
        {
            var actualOp = +value;
            var actualFu = value.Plus();

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Increment(Integer value, Integer expected)
        {
            var actualOp = value; ++actualOp;
            var actualFu = value.Increment();

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void AddInt(Integer left, int right, Integer expected)
        {
            var actualOp = left + right;
            var actualFu = left.Add(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Add(Integer left, Integer right, Integer expected)
        {
            var actualOp = left + right;
            var actualFu = left.Add(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Negate(Integer value, Integer expected)
        {
            var actualOp = -value;
            var actualFu = value.Negate();

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Decrement(Integer value, Integer expected)
        {
            var actualOp = value; --actualOp;
            var actualFu = value.Decrement();

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void SubtractInt(Integer left, int right, Integer expected)
        {
            var actualOp = left - right;
            var actualFu = left.Subtract(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Subtract(Integer left, Integer right, Integer expected)
        {
            var actualOp = left - right;
            var actualFu = left.Subtract(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Square(Integer value, Integer expected)
        {
            var actualOp = value * value;
            var actualFu = value.Multiply(value);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void MultiplyInt(Integer left, int right, Integer expected)
        {
            var actualOp = left * right;
            var actualFu = left.Multiply(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Multiply(Integer left, Integer right, Integer expected)
        {
            var actualOp = left * right;
            var actualFu = left.Multiply(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void DivideInt(Integer left, int right, Integer expected)
        {
            var actualOp = left / right;
            var actualFu = left.Divide(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Divide(Integer left, Integer right, Integer expected)
        {
            var actualOp = left / right;
            var actualFu = left.Divide(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void RemainderInt(Integer left, int right, int expected)
        {
            var actualOp = left % right;
            var actualFu = left.Remainder(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void Remainder(Integer left, Integer right, Integer expected)
        {
            var actualOp = left % right;
            var actualFu = left.Remainder(right);

            Assert.Equal(expected, actualOp);
            Assert.Equal(expected, actualFu);
        }

        [Theory]
        [IntegerData]
        public void String(Integer value, string expected)
        {
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Parse(string value, Integer expected)
        {
            var actual = Integer.Parse(value);

            Assert.Equal(expected, actual);
        }
    }
}
