using System;
using Xunit;

namespace NeinMath.Tests
{
    public class IntegerConverterTest
    {
        [Theory]
        [IntegerData]
        public void ToByteArray(Integer value, byte[] expected)
        {
            var actual = IntegerConverter.ToByteArray(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void FromByteArray(byte[] value, Integer expected)
        {
            var actual = IntegerConverter.FromByteArray(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void ToDecimalString(Integer value, string expected)
        {
            var actual = IntegerConverter.ToDecimalString(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void FromDecimalString(string value, Integer expected)
        {
            var actual = IntegerConverter.FromDecimalString(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void ToHexString(Integer value, string expected)
        {
            var actual = IntegerConverter.ToHexString(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [IntegerData]
        public void FromHexString(string value, Integer expected)
        {
            var actual = IntegerConverter.FromHexString(value);

            Assert.Equal(expected, actual);
        }
    }
}
