using Xunit;

#pragma warning disable CA1305

namespace NeinMath.Tests
{
    public class IntegerConverterData : IntegerDataSource
    {
        public TheoryData<Integer, byte[]> ToByteArray()
        {
            var data = new TheoryData<Integer, byte[]>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), i.ToByteArray());
            return data;
        }

        public TheoryData<byte[], Integer> FromByteArray()
        {
            var data = new TheoryData<byte[], Integer>();
            foreach (var i in BigIntegers())
                data.Add(i.ToByteArray(), ToInteger(i));
            return data;
        }

        public TheoryData<Integer, string> ToDecimalString()
        {
            var data = new TheoryData<Integer, string>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), i.ToString("R"));
            return data;
        }

        public TheoryData<string, Integer> FromDecimalString()
        {
            var data = new TheoryData<string, Integer>();
            foreach (var i in BigIntegers())
                data.Add(i.ToString("R"), ToInteger(i));
            return data;
        }

        public TheoryData<Integer, string> ToHexString()
        {
            var data = new TheoryData<Integer, string>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), i.ToString("X"));
            return data;
        }

        public TheoryData<string, Integer> FromHexString()
        {
            var data = new TheoryData<string, Integer>();
            foreach (var i in BigIntegers())
                data.Add(i.ToString("X"), ToInteger(i));
            return data;
        }
    }
}
