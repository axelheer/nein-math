using System;

namespace NeinMath
{
    /// <summary>
    /// Represents an arbitrarily large signed integer.
    /// </summary>
    public struct Integer : IComparable,
                            IComparable<int>, IComparable<Integer>,
                            IEquatable<int>, IEquatable<Integer>
    {
        internal readonly uint[] bits;
        internal readonly int length;
        internal readonly bool sign;

        internal Integer(uint[] bits, int length, bool sign)
        {
            // no leading zeros; and zero has no sign
            length = Bits.Length(bits, length);
            sign = sign && length > 0;

            this.bits = bits;
            this.length = length;
            this.sign = sign;
        }

        /// <summary>
        /// Defines an implicit conversion of a signed 32-bit integer to an
        /// Integer.
        /// </summary>
        /// <param name="value">The value to convert to an Integer.</param>
        /// <returns>
        /// An Integer that contains the value of the value parameter.
        /// </returns>
        public static implicit operator Integer(int value)
        {
            return new Integer(new[] { Bits.Abs(value) }, 1, value < 0);
        }

        /// <summary>
        /// Defines an explicit conversion of an Integer to a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="value">
        /// The value to convert to a signed 32-bit integer.
        /// </param>
        /// <returns>
        /// A signed 32-bit integer that contains the value of the value
        /// parameter.
        /// </returns>
        public static explicit operator int(Integer value)
        {
            var num = value.length != 0
                ? (int)value.bits[0]
                : 0;
            if (value.length > 1 || num < 0)
                throw new OverflowException();
            if (value.sign)
                num *= -1;
            return num;
        }

        /// <summary>
        /// Performs a bitwise AND operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise AND operation.</returns>
        public Integer BitwiseAnd(int right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var rightPad = right < 0
                ? 0xFFFFFFFF : 0x00000000;

            var result = Calc.And(leftBits, length, (uint)right, rightPad);
            var signed = sign & (right < 0);
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise AND operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise AND operation.</returns>
        public static Integer operator &(Integer left, int right)
        {
            return left.BitwiseAnd(right);
        }

        /// <summary>
        /// Performs a bitwise AND operation on two Integers.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise AND operation.</returns>
        public Integer BitwiseAnd(Integer right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var leftPad = sign
                ? 0xFFFFFFFF : 0x00000000;
            var rightBits = right.sign
                ? Bits.TwosComplement(right.bits, right.length) : right.bits;
            var rightPad = right.sign
                ? 0xFFFFFFFF : 0x00000000;

            var result = length < right.length
                ? Calc.And(rightBits, right.length, leftBits, length, leftPad)
                : Calc.And(leftBits, length, rightBits, right.length, rightPad);
            var signed = sign & right.sign;
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise AND operation on two Integers.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise AND operation.</returns>
        public static Integer operator &(Integer left, Integer right)
        {
            return left.BitwiseAnd(right);
        }

        /// <summary>
        /// Performs a bitwise OR operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise OR operation.</returns>
        public Integer BitwiseOr(int right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var rightPad = right < 0
                ? 0xFFFFFFFF : 0x00000000;

            var result = Calc.Or(leftBits, length, (uint)right, rightPad);
            var signed = sign | (right < 0);
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise OR operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise OR operation.</returns>
        public static Integer operator |(Integer left, int right)
        {
            return left.BitwiseOr(right);
        }

        /// <summary>
        /// Performs a bitwise OR operation on two Integers.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise OR operation.</returns>
        public Integer BitwiseOr(Integer right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var leftPad = sign
                ? 0xFFFFFFFF : 0x00000000;
            var rightBits = right.sign
                ? Bits.TwosComplement(right.bits, right.length) : right.bits;
            var rightPad = right.sign
                ? 0xFFFFFFFF : 0x00000000;

            var result = length < right.length
                ? Calc.Or(rightBits, right.length, leftBits, length, leftPad)
                : Calc.Or(leftBits, length, rightBits, right.length, rightPad);
            var signed = sign | right.sign;
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise OR operation on two Integers.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise OR operation.</returns>
        public static Integer operator |(Integer left, Integer right)
        {
            return left.BitwiseOr(right);
        }

        /// <summary>
        /// Performs a bitwise XOR operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise XOR operation.</returns>
        public Integer Xor(int right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var rightPad = right < 0
                ? 0xFFFFFFFF : 0x00000000;

            var result = Calc.Xor(leftBits, length, (uint)right, rightPad);
            var signed = sign ^ (right < 0);
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise XOR operation on an Integer and a signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise XOR operation.</returns>
        public static Integer operator ^(Integer left, int right)
        {
            return left.Xor(right);
        }

        /// <summary>
        /// Performs a bitwise XOR operation on two Integers.
        /// </summary>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise XOR operation.</returns>
        public Integer Xor(Integer right)
        {
            var leftBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var leftPad = sign
                ? 0xFFFFFFFF : 0x00000000;
            var rightBits = right.sign
                ? Bits.TwosComplement(right.bits, right.length) : right.bits;
            var rightPad = right.sign
                ? 0xFFFFFFFF : 0x00000000;

            var result = length < right.length
                ? Calc.Xor(rightBits, right.length, leftBits, length, leftPad)
                : Calc.Xor(leftBits, length, rightBits, right.length, rightPad);
            var signed = sign ^ right.sign;
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// Performs a bitwise XOR operation on two Integers.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise XOR operation.</returns>
        public static Integer operator ^(Integer left, Integer right)
        {
            return left.Xor(right);
        }

        /// <summary>
        /// Returns the bitwise one's complement of an Integer.
        /// </summary>
        /// <returns>The bitwise one's complement.</returns>
        public Integer OnesComplement()
        {
            var selfBits = sign
                ? Bits.TwosComplement(bits, length) : bits;

            var result = Bits.OnesComplement(selfBits, length);
            var signed = !sign;
            if (signed)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, signed);
        }

        /// <summary>
        /// /// Returns the bitwise one's complement of an Integer.
        /// </summary>
        /// <param name="value">An Integer.</param>
        /// <returns>The bitwise one's complement.</returns>
        public static Integer operator ~(Integer value)
        {
            return value.OnesComplement();
        }

        /// <summary>
        /// Shifts an Integer a specified number of bits to the right.
        /// </summary>
        /// <param name="shift">
        /// The number of bits to shift to the right.
        /// </param>
        /// <returns>
        /// An Integer that has been shifted to the right by the specified
        /// number of bits.
        /// </returns>
        public Integer RightShift(int shift)
        {
            var selfBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var shiftPad = sign
                ? 0xFFFFFFFF : 0x00000000;

            var result = Calc.Shift(selfBits, length, -shift, shiftPad);
            if (sign)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, sign);
        }

        /// <summary>
        /// Shifts an Integer a specified number of bits to the right.
        /// </summary>
        /// <param name="value">The value whose bits are to be shifted.</param>
        /// <param name="shift">
        /// The number of bits to shift to the right.
        /// </param>
        /// <returns>
        /// An Integer that has been shifted to the right by the specified
        /// number of bits.
        /// </returns>
        public static Integer operator >>(Integer value, int shift)
        {
            return value.RightShift(shift);
        }

        /// <summary>
        /// Shifts an Integer a specified number of bits to the left.
        /// </summary>
        /// <param name="shift">
        /// The number of bits to shift to the left.
        /// </param>
        /// <returns>
        /// An Integer that has been shifted to the left by the specified
        /// number of bits.
        /// </returns>
        public Integer LeftShift(int shift)
        {
            var selfBits = sign
                ? Bits.TwosComplement(bits, length) : bits;
            var shiftPad = sign
                ? 0xFFFFFFFF : 0x00000000;

            var result = Calc.Shift(selfBits, length, shift, shiftPad);
            if (sign)
                result = Bits.TwosComplement(result, result.Length);

            return new Integer(result, result.Length, sign);
        }

        /// <summary>
        /// Shifts an Integer a specified number of bits to the left.
        /// </summary>
        /// <param name="value">The value whose bits are to be shifted.</param>
        /// <param name="shift">
        /// The number of bits to shift to the left.
        /// </param>
        /// <returns>
        /// An Integer that has been shifted to the left by the specified
        /// number of bits.
        /// </returns>
        public static Integer operator <<(Integer value, int shift)
        {
            return value.LeftShift(shift);
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and a specified
        /// object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>
        /// true, if the obj parameter is an Integer or a type capable of
        /// conversion to an Integer, and its value is equal to the Integer;
        /// otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Integer))
                return false;
            return Equals((Integer)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and a signed
        /// 32-bit integer have the same value.
        /// </summary>
        /// <param name="other">The signed 32-bit integer to compare.</param>
        /// <returns>
        /// true, if the signed 32-bit integer and the Integer have the same
        /// value; otherwise, false.
        /// </returns>
        public bool Equals(int other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and a specified
        /// Integer have the same value.
        /// </summary>
        /// <param name="other">The Integer to compare.</param>
        /// <returns>
        /// true, if the Integer and other have the same value; otherwise,
        /// false.
        /// </returns>
        public bool Equals(Integer other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and a signed
        /// 32-bit integer have the same value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if the left and right parameters have the same value;
        /// otherwise, false.
        /// </returns>
        public static bool operator ==(Integer left, int right)
        {
            return left.CompareTo(right) == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and another
        /// Integer have the same value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if the left and right parameters have the same value;
        /// otherwise, false.
        /// </returns>
        public static bool operator ==(Integer left, Integer right)
        {
            return left.CompareTo(right) == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and a signed
        /// 32-bit integer have different values.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if the left and right parameters have different values;
        /// otherwise, false.
        /// </returns>
        public static bool operator !=(Integer left, int right)
        {
            return left.CompareTo(right) != 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer and another
        /// Integer have different values.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if the left and right parameters have different values;
        /// otherwise, false.
        /// </returns>
        public static bool operator !=(Integer left, Integer right)
        {
            return left.CompareTo(right) != 0;
        }

        /// <summary>
        /// Compares the Integer to a specified object and returns an integer
        /// that indicates whether the value of the Integer is less than, equal
        /// to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>
        /// An integer that indicates the relationship of the Integer to the
        /// obj parameter.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (!(obj is Integer))
                throw new ArgumentOutOfRangeException("obj");
            return CompareTo((Integer)obj);
        }

        /// <summary>
        /// Compares the Integer to a signed 32-bit integer and returns an
        /// integer that indicates whether the value of the Integer is less
        /// than, equal to, or greater than the value of the signed 32-bit
        /// integer.
        /// </summary>
        /// <param name="other">The signed 32-bit integer to compare.</param>
        /// <returns>
        /// An integer that indicates the relationship of the Integer to the
        /// other parameter.
        /// </returns>
        public int CompareTo(int other)
        {
            if (sign && other >= 0)
                return -1;
            if (!sign && other < 0)
                return 1;

            var result = Bits.Compare(bits, length, Bits.Abs(other));
            if (sign)
                result *= -1;

            return result;
        }

        /// <summary>
        /// Compares the Integer to another Integer and returns an integer that
        /// indicates whether the value of the Integer is less than, equal to,
        /// or greater than the value of the Integer.
        /// </summary>
        /// <param name="other">The Integer to compare.</param>
        /// <returns>
        /// An integer that indicates the relationship of the Integer to the
        /// other parameter.
        /// </returns>
        public int CompareTo(Integer other)
        {
            if (sign && !other.sign)
                return -1;
            if (!sign && other.sign)
                return 1;

            var result = Bits.Compare(bits, length, other.bits, other.length);
            if (sign)
                result *= -1;

            return result;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is less than a
        /// signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is less than right; otherwise, false.
        /// </returns>
        public static bool operator <(Integer left, int right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is less than
        /// another Integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is less than right; otherwise, false.
        /// </returns>
        public static bool operator <(Integer left, Integer right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is less than or
        /// equal to a signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is less than or equal to right; otherwise, false.
        /// </returns>
        public static bool operator <=(Integer left, int right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is less than or
        /// equal to another Integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is less than or equal to right; otherwise, false.
        /// </returns>
        public static bool operator <=(Integer left, Integer right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is greater than a
        /// signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is greater than right; otherwise, false.
        /// </returns>
        public static bool operator >(Integer left, int right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is greater than
        /// another Integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is greater than right; otherwise, false.
        /// </returns>
        public static bool operator >(Integer left, Integer right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is greater than
        /// or equal to a signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is greater than or equal to right; otherwise, false.
        /// </returns>
        public static bool operator >=(Integer left, int right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Returns a value that indicates whether an Integer is greater than
        /// or equal to another Integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// true, if left is greater than or equal to right; otherwise, false.
        /// </returns>
        public static bool operator >=(Integer left, Integer right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Returns the value of an Integer.
        /// </summary>
        /// <returns>The value of the Integer.</returns>
        public Integer Plus()
        {
            return new Integer(bits, length, sign);
        }

        /// <summary>
        /// Returns the value of an Integer.
        /// </summary>
        /// <param name="value">An Integer.</param>
        /// <returns>The value of the value parameter.</returns>
        public static Integer operator +(Integer value)
        {
            return value.Plus();
        }

        /// <summary>
        /// Increments an Integer by 1.
        /// </summary>
        /// <returns>An Integer that has been incremented by 1.</returns>
        public Integer Increment()
        {
            return Add(1);
        }

        /// <summary>
        /// Increments an Integer by 1.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>An Integer that has been incremented by 1.</returns>
        public static Integer operator ++(Integer value)
        {
            return value.Increment();
        }

        /// <summary>
        /// Adds the values of an Integer and a signed 32-bit integer.
        /// </summary>
        /// <param name="right">The value to add.</param>
        /// <returns>The sum of the Integer and right.</returns>
        public Integer Add(int right)
        {
            if (length == 0)
                return ((Integer)right).Plus();
            if (sign != (right < 0))
                return Negate().Subtract(right).Negate();

            var result = Calc.Add(bits, length, Bits.Abs(right));
            return new Integer(result, result.Length, sign);
        }

        /// <summary>
        /// Adds the values of an Integer and a signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static Integer operator +(Integer left, int right)
        {
            return left.Add(right);
        }

        /// <summary>
        /// Adds the values of an Integer and another Integer.
        /// </summary>
        /// <param name="right">The value to add.</param>
        /// <returns>The sum of the Integer and right.</returns>
        public Integer Add(Integer right)
        {
            if (right.length == 0)
                return Plus();
            if (sign != right.sign)
                return Subtract(right.Negate());

            var result = length < right.length
                ? Calc.Add(right.bits, right.length, bits, length)
                : Calc.Add(bits, length, right.bits, right.length);

            return new Integer(result, result.Length, sign);
        }

        /// <summary>
        /// Adds the values of an Integer and another Integer.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static Integer operator +(Integer left, Integer right)
        {
            return left.Add(right);
        }

        /// <summary>
        /// Negates an Integer value.
        /// </summary>
        /// <returns>An Integer that has been negated.</returns>
        public Integer Negate()
        {
            return new Integer(bits, length, !sign);
        }

        /// <summary>
        /// Negates an Integer value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>An Integer that has been negated.</returns>
        public static Integer operator -(Integer value)
        {
            return value.Negate();
        }

        /// <summary>
        /// Decrements an Integer by 1.
        /// </summary>
        /// <returns>An Integer that has been decremented by 1.</returns>
        public Integer Decrement()
        {
            return Subtract(1);
        }

        /// <summary>
        /// Decrements an Integer by 1.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>An Integer that has been decremented by 1.</returns>
        public static Integer operator --(Integer value)
        {
            return value.Decrement();
        }

        /// <summary>
        /// Subtracts a signed 32-bit integer from an Integer.
        /// </summary>
        /// <param name="right">The value to subtract.</param>
        /// <returns>
        /// The result of subtracting right from the Integer.
        /// </returns>
        public Integer Subtract(int right)
        {
            if (length == 0)
                return ((Integer)right).Negate();
            if (sign != (right < 0))
                return Negate().Add(right).Negate();
            if (length == 1 && bits[0] < Bits.Abs(right))
                return new Integer(new uint[] { Bits.Abs(right)
                    - bits[0] }, 1, !sign);

            var result = Calc.Subtract(bits, length, Bits.Abs(right));
            return new Integer(result, result.Length, sign);
        }

        /// <summary>
        /// Subtracts a signed 32-bit integer from an Integer.
        /// </summary>
        /// <param name="left">The value to subtract from.</param>
        /// <param name="right">The value to subtract.</param>
        /// <returns>The result of subtracting right from left.</returns>
        public static Integer operator -(Integer left, int right)
        {
            return left.Subtract(right);
        }

        /// <summary>
        /// Subtracts an Integer from another Integer.
        /// </summary>
        /// <param name="right">The value to subtract.</param>
        /// <returns>
        /// The result of subtracting right from the Integer.
        /// </returns>
        public Integer Subtract(Integer right)
        {
            if (right.length == 0)
                return Plus();
            if (sign != right.sign)
                return Add(right.Negate());

            var diff = Bits.Compare(bits, length, right.bits, right.length);
            var result = diff < 0
                ? Calc.Subtract(right.bits, right.length, bits, length)
                : Calc.Subtract(bits, length, right.bits, right.length);

            return new Integer(result, result.Length, diff < 0 ? !sign : sign);
        }

        /// <summary>
        /// Subtracts an Integer from another Integer.
        /// </summary>
        /// <param name="left">The value to subtract from.</param>
        /// <param name="right">The value to subtract.</param>
        /// <returns>The result of subtracting right from left.</returns>
        public static Integer operator -(Integer left, Integer right)
        {
            return left.Subtract(right);
        }

        /// <summary>
        /// Multiplies the values of an Integer and a signed 32-bit integer.
        /// </summary>
        /// <param name="right">The value to multiply with.</param>
        /// <returns>The product of the Integer and right.</returns>
        public Integer Multiply(int right)
        {
            var result = Calc.Multiply(bits, length, Bits.Abs(right));
            return new Integer(result, result.Length, sign ^ (right < 0));
        }

        /// <summary>
        /// Multiplies the values of an Integer and a signed 32-bit integer.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of left and right.</returns>
        public static Integer operator *(Integer left, int right)
        {
            return left.Multiply(right);
        }

        /// <summary>
        /// Multiplies the values of an Integer and another Integer.
        /// </summary>
        /// <param name="right">The value to multiply with.</param>
        /// <returns>The product of the Integer and right.</returns>
        public Integer Multiply(Integer right)
        {
            var result = bits == right.bits
                ? Calc.Square(bits, length)
                : Calc.Multiply(bits, length, right.bits, right.length);
            return new Integer(result, result.Length, sign ^ right.sign);
        }

        /// <summary>
        /// Multiplies the values of an Integer and another Integer.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of left and right.</returns>
        public static Integer operator *(Integer left, Integer right)
        {
            return left.Multiply(right);
        }

        /// <summary>
        /// Divides an Integer by a signed 32-bit integer.
        /// </summary>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public Integer Divide(int right)
        {
            if (right == 0)
                throw new DivideByZeroException();

            var remainder = default(uint);
            var result = Calc.Divide(bits, length, Bits.Abs(right),
                out remainder);
            return new Integer(result, result.Length, sign ^ (right < 0));
        }

        /// <summary>
        /// Divides an Integer by a signed 32-bit integer.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public static Integer operator /(Integer left, int right)
        {
            return left.Divide(right);
        }

        /// <summary>
        /// Divides an Integer by another Integer.
        /// </summary>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public Integer Divide(Integer right)
        {
            if (right.length == 0)
                throw new DivideByZeroException();
            if (length < right.length)
                return new Integer(null, 0, false);

            var remainder = default(uint[]);
            var result = Calc.Divide(bits, length, right.bits, right.length,
                out remainder);
            return new Integer(result, result.Length, sign ^ right.sign);
        }

        /// <summary>
        /// Divides an Integer by another Integer.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public static Integer operator /(Integer left, Integer right)
        {
            return left.Divide(right);
        }

        /// <summary>
        /// Returns the remainder that results from dividing an Integer by a
        /// signed 32-bit integer.
        /// </summary>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public int Remainder(int right)
        {
            if (right == 0)
                throw new DivideByZeroException();

            var result = Calc.Remainder(bits, length, Bits.Abs(right));
            return sign
                ? -1 * (int)result
                : (int)result;
        }

        /// <summary>
        /// Returns the remainder that results from dividing an Integer by a
        /// signed 32-bit integer.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static int operator %(Integer left, int right)
        {
            return left.Remainder(right);
        }

        /// <summary>
        /// Returns the remainder that results from dividing an Integer by
        /// another Integer.
        /// </summary>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public Integer Remainder(Integer right)
        {
            if (right.length == 0)
                throw new DivideByZeroException();
            if (length < right.length)
                return new Integer(bits, length, sign);

            var remainder = default(uint[]);
            var result = Calc.Divide(bits, length, right.bits, right.length,
                out remainder);
            return new Integer(remainder, remainder.Length, sign);
        }

        /// <summary>
        /// Returns the remainder that results from dividing an Integer by
        /// another Integer.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static Integer operator %(Integer left, Integer right)
        {
            return left.Remainder(right);
        }

        /// <summary>
        /// Converts an Integer to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the Integer.</returns>
        public override string ToString()
        {
            return IntegerConverter.ToDecimalString(this);
        }

        /// <summary>
        /// Converts the string representation of a number to its Integer
        /// equivalent.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A value that is equivalent to the number specified in the value
        /// parameter.
        /// </returns>
        public static Integer Parse(string value)
        {
            return IntegerConverter.FromDecimalString(value);
        }

        /// <summary>
        /// Returns the hash code for an Integer.
        /// </summary>
        /// <returns>A signed 32-bit integer hash code.</returns>
        public override int GetHashCode()
        {
            var hash = 0U;
            for (var i = 0; i < length; i++)
                hash ^= bits[i];
            return (int)hash;
        }
    }
}
