using System;
using Xunit;

namespace NeinMath.Tests
{
    public sealed class Immutability : IDisposable
    {
        readonly Integer left;
        readonly Integer right;
        readonly Integer other;

        readonly string leftValue;
        readonly string rightValue;
        readonly string otherValue;

        Immutability(Integer value)
            : this(value, default(Integer))
        {
        }

        Immutability(Integer left, Integer right)
            : this(left, right, default(Integer))
        {
        }

        Immutability(Integer left, Integer right, Integer other)
        {
            leftValue = left.ToString();
            rightValue = right.ToString();
            otherValue = other.ToString();

            this.left = left;
            this.right = right;
            this.other = other;
        }

        public static IDisposable Guard(Integer value)
        {
            return new Immutability(value);
        }

        public static IDisposable Guard(Integer left, Integer right)
        {
            return new Immutability(left, right);
        }

        public static IDisposable Guard(Integer left, Integer right, Integer other)
        {
            return new Immutability(left, right, other);
        }

        public void Dispose()
        {
            Assert.Equal(leftValue, left.ToString());
            Assert.Equal(rightValue, right.ToString());
            Assert.Equal(otherValue, other.ToString());
        }
    }
}
