using System;
using Xunit;

namespace NeinMath.Tests
{
    public class IntegerFunctionsTest
    {
        [Theory]
        [IntegerData]
        public void Sgn(Integer value, int expected)
        {
            var actual = value.Sgn();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Abs(Integer value, Integer expected)
        {
            var actual = value.Abs();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Min(Integer left, Integer right, Integer expected)
        {
            var actual = left.Min(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Max(Integer left, Integer right, Integer expected)
        {
            var actual = left.Max(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Gcd(Integer left, Integer right, Integer expected)
        {
            var actual = left.Gcd(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Lcm(Integer left, Integer right, Integer expected)
        {
            var actual = left.Lcm(right);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void ModInv(Integer value, Integer modulus)
        {
            var result = value.ModInv(modulus);
            var common = value.Gcd(modulus);

            if (value % modulus != 0 && common == 1)
            {
                var check = (value * result) % modulus;
                if (check < 0)
                    check = check + modulus;

                Assert.Equal(1, check);
            }
            else
            {
                Assert.Equal(0, result);
            }
        }

        [Theory]
        [IntegerData]
        public void Pow(Integer value, int power, Integer expected)
        {
            var actual = value.Pow(power);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void Log(Integer value, double baseValue, double expected)
        {
            var actual = value.Log(baseValue);

            Assert.Equal(expected, actual, 8);
        }

        [Theory]
        [IntegerData]
        public void ModPow(Integer value, Integer power, Integer modulus,
                           Integer expected)
        {
            var actual = value.ModPow(power, modulus);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void DivRem(Integer left, Integer right,
                           Integer expected, Integer expectedRemainder)
        {
            var actualRemainder = default(Integer);
            var actual = left.DivRem(right, out actualRemainder);

            Assert.Equal(expectedRemainder, actualRemainder);
            Assert.Equal(expected, actual);
        }
    }
}
