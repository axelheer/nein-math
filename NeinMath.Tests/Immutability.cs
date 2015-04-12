using System;
using Xunit;

namespace NeinMath.Tests
{
    public sealed class Immutability : IDisposable
    {
        private readonly Integer left;
        private readonly Integer right;
        private readonly Integer other;

        private readonly string leftValue;
        private readonly string rightValue;
        private readonly string otherValue;

        private Immutability(Integer value)
            : this(value, default(Integer))
        {
        }

        private Immutability(Integer left, Integer right)
            : this(left, right, default(Integer))
        {
        }

        private Immutability(Integer left, Integer right, Integer other)
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
