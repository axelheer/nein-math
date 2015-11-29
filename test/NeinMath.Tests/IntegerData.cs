using Xunit;

namespace NeinMath.Tests
{
    public class IntegerData : IntegerDataSource
    {
        public TheoryData<Integer, int, Integer> BitwiseAndInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 & i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> BitwiseAnd()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 & i.Item2));
            return data;
        }

        public TheoryData<Integer, int, Integer> BitwiseOrInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 | i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> BitwiseOr()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 | i.Item2));
            return data;
        }

        public TheoryData<Integer, int, Integer> XorInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 ^ i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Xor()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 ^ i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer> OnesComplement()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(~i));
            return data;
        }

        public TheoryData<Integer, int, Integer> RightShift()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers(x => x % 100)))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 >> i.Item2));
            return data;
        }

        public TheoryData<Integer, int, Integer> LeftShift()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers(x => x % 100)))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 << i.Item2));
            return data;
        }

        public TheoryData<Integer, int, bool> EqualsInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 == i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> Equals()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 == i.Item2);
            return data;
        }

        public TheoryData<Integer, int, bool> NotEqualsInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 != i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> NotEquals()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 != i.Item2);
            return data;
        }

        public TheoryData<Integer, int, int> CompareToInt()
        {
            var data = new TheoryData<Integer, int, int>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    i.Item1.CompareTo(i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, int> CompareTo()
        {
            var data = new TheoryData<Integer, Integer, int>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1.CompareTo(i.Item2));
            return data;
        }

        public TheoryData<Integer, int, bool> LessThanInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 < i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> LessThan()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 < i.Item2);
            return data;
        }

        public TheoryData<Integer, int, bool> LessThanOrEqualInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 <= i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> LessThanOrEqual()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 <= i.Item2);
            return data;
        }

        public TheoryData<Integer, int, bool> GreaterThanInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 > i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> GreaterThan()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 > i.Item2);
            return data;
        }

        public TheoryData<Integer, int, bool> GreaterThanOrEqualInt()
        {
            var data = new TheoryData<Integer, int, bool>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2, i.Item1 >= i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer, bool> GreaterThanOrEqual()
        {
            var data = new TheoryData<Integer, Integer, bool>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    i.Item1 >= i.Item2);
            return data;
        }

        public TheoryData<Integer, Integer> Plus()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(+i));
            return data;
        }

        public TheoryData<Integer, Integer> Increment()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(i + 1));
            return data;
        }

        public TheoryData<Integer, int, Integer> AddInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 + i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Add()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 + i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer> Negate()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(-i));
            return data;
        }

        public TheoryData<Integer, Integer> Decrement()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(i - 1));
            return data;
        }

        public TheoryData<Integer, int, Integer> SubtractInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 - i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Subtract()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 - i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer> Square()
        {
            var data = new TheoryData<Integer, Integer>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), ToInteger(i * i));
            return data;
        }

        public TheoryData<Integer, int, Integer> MultiplyInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers()))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 * i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Multiply()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers()))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 * i.Item2));
            return data;
        }

        public TheoryData<Integer, int, Integer> DivideInt()
        {
            var data = new TheoryData<Integer, int, Integer>();
            foreach (var i in Items(BigIntegers(), Integers(x => x != 0)))
                data.Add(ToInteger(i.Item1), i.Item2,
                    ToInteger(i.Item1 / i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Divide()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers(x => x != 0)))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 / i.Item2));
            return data;
        }

        public TheoryData<Integer, int, int> RemainderInt()
        {
            var data = new TheoryData<Integer, int, int>();
            foreach (var i in Items(BigIntegers(), Integers(x => x != 0)))
                data.Add(ToInteger(i.Item1), i.Item2,
                    (int)(i.Item1 % i.Item2));
            return data;
        }

        public TheoryData<Integer, Integer, Integer> Remainder()
        {
            var data = new TheoryData<Integer, Integer, Integer>();
            foreach (var i in Items(BigIntegers(), BigIntegers(x => x != 0)))
                data.Add(ToInteger(i.Item1), ToInteger(i.Item2),
                    ToInteger(i.Item1 % i.Item2));
            return data;
        }

        public TheoryData<Integer, string> String()
        {
            var data = new TheoryData<Integer, string>();
            foreach (var i in BigIntegers())
                data.Add(ToInteger(i), i.ToString("R"));
            return data;
        }

        public TheoryData<string, Integer> Parse()
        {
            var data = new TheoryData<string, Integer>();
            foreach (var i in BigIntegers())
                data.Add(i.ToString("R"), ToInteger(i));
            return data;
        }
    }
}
