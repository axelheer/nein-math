using System;

namespace NeinMath
{
    /// <summary>
    /// Provides common mathematical functions for Integers.
    /// </summary>
    public static class IntegerFunctions
    {
        /// <summary>
        /// Gets a number that indicates the sign of an Integer.
        /// </summary>
        /// <param name="value">The Integer.</param>
        /// <returns>A number that indicates the sign of the Integer.</returns>
        public static int Sgn(this Integer value)
        {
            return value.CompareTo(0);
        }

        /// <summary>
        /// Gets the absolute value of an Integer.
        /// </summary>
        /// <param name="value">The Integer.</param>
        /// <returns>The absolute value of the Integer.</returns>
        public static Integer Abs(this Integer value)
        {
            return value < 0 ? -value : value;
        }

        /// <summary>
        /// Returns the smaller of two Integers.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// The left or right parameter, whichever is smaller.
        /// </returns>
        public static Integer Min(this Integer left, Integer right)
        {
            return left < right ? left : right;
        }

        /// <summary>
        /// Returns the larger of two Integers.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// The left or right parameter, whichever is larger.
        /// </returns>
        public static Integer Max(this Integer left, Integer right)
        {
            return left < right ? right : left;
        }

        /// <summary>
        /// Finds the greatest common divisor of two Integers.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The greatest common divisor of left and right.</returns>
        public static Integer Gcd(this Integer left, Integer right)
        {
            var a = Abs(left);
            var b = Abs(right);

            if (a < b)
                return Lehmer.Gcd(b, a);
            return Lehmer.Gcd(a, b);
        }

        /// <summary>
        /// Finds the least common multiple of two Integers.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The least common multiple of left and right.</returns>
        public static Integer Lcm(this Integer left, Integer right)
        {
            return Abs(left * right) / Gcd(left, right);
        }

        /// <summary>
        /// Performs modulus inversion on an Integer.
        /// </summary>
        /// <param name="value">The Integer to inverse.</param>
        /// <param name="modulus">The Integer by which to divide.</param>
        /// <returns>The result of inversing the Integer; if any.</returns>
        public static Integer ModInv(this Integer value, Integer modulus)
        {
            if (modulus < 1)
                throw new ArgumentOutOfRangeException(nameof(modulus));

            if (Abs(value) >= modulus)
                value = value % modulus;
            if (value < 0)
                value = value + modulus;
            return Lehmer.Inv(modulus, value);
        }

        /// <summary>
        /// Raises an Integer to the power of a specified value.
        /// </summary>
        /// <param name="value">
        /// The Integer to raise to the exponent power.
        /// </param>
        /// <param name="power">The exponent to raise the Integer by.</param>
        /// <returns>
        /// The result of raising the Integer to the exponent power.
        /// </returns>
        public static Integer Pow(this Integer value, int power)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException(nameof(power));

            Integer result = 1;

            while (power != 0)
            {
                if (power % 2 == 1)
                    result = value * result;
                value = value * value;
                power = power >> 1;
            }

            return result;
        }

        /// <summary>
        /// Returns the logarithm of a specified Integer in a specified base.
        /// </summary>
        /// <param name="value">
        /// The Integer whose logarithm is to be found.
        /// </param>
        /// <param name="baseValue">The base of the logarithm.</param>
        /// <returns>The result of finding the logarithm.</returns>
        public static double Log(this Integer value, double baseValue = Math.E)
        {
            if (value < 0)
                return double.NaN;
            if (baseValue < 0)
                return double.NaN;

#pragma warning disable RECS0018

            if (baseValue == 0)
                return value != 1 ? double.NaN : 0;
            if (double.IsPositiveInfinity(baseValue))
                return value != 1 ? double.NaN : 0;
            if (double.IsNaN(baseValue))
                return double.NaN;
            if (baseValue == 1)
                return double.NaN;

#pragma warning restore RECS0018

            if (value == 0)
                return baseValue < 1
                    ? double.PositiveInfinity
                    : double.NegativeInfinity;

            // extract most significant bits
            var h = (ulong)value.bits[value.length - 1];
            var m = (ulong)(value.length > 1
                ? value.bits[value.length - 2] : 0);
            var l = (ulong)(value.length > 2
                ? value.bits[value.length - 3] : 0);

            // combine with bit count (log2)
            var z = Bits.LeadingZeros((uint)h);
            var y = value.length * 32 - z - 1.0;
            var x = (h << 32 + z) | (m << z) | (l >> 32 - z);

            return (y + Math.Log(x, 2) - 63) / Math.Log(baseValue, 2);
        }

        /// <summary>
        /// Performs modulus division on an Integer raised to the power of
        /// another Integer.
        /// </summary>
        /// <param name="value">
        /// The Integer to raise to the exponent power.
        /// </param>
        /// <param name="power">The exponent to raise the Integer by.</param>
        /// <param name="modulus">The Integer by which to divide.</param>
        /// <returns>
        /// The result of raising the Integer to the exponent power.
        /// </returns>
        public static Integer ModPow(this Integer value, Integer power,
                                     Integer modulus)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException(nameof(power));
            if (modulus < 1)
                throw new ArgumentOutOfRangeException(nameof(modulus));

            var barrett = Barrett.Begin(modulus);

            var v = new Integer[256];
            v[0] = 1;
            v[1] = barrett.Reduce(value);
            v[2] = barrett.Reduce(v[1] * v[1]);

            for (var j = 4; j < v.Length; j *= 2)
                v[j] = barrett.Reduce(v[j / 2] * v[j / 2]);
            for (var i = 3; i < v.Length; i += 2)
            {
                v[i] = barrett.Reduce(v[i - 1] * v[1]);
                for (var j = i * 2; j < v.Length; j *= 2)
                    v[j] = barrett.Reduce(v[j / 2] * v[j / 2]);
            }

            var p = power.ToByteArray();

            Integer result = v[p[p.Length - 1]];
            for (var i = p.Length - 2; i >= 0; i--)
            {
                for (var j = 0; j < 8; j++)
                    result = barrett.Reduce(result * result);
                result = barrett.Reduce(result * v[p[i]]);
            }

            return result;
        }

        /// <summary>
        /// Divides an Integer value by another, returns the result, and
        /// returns the remainder in an output parameter.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <param name="remainder">
        /// The remainder that results from the division.
        /// </param>
        /// <returns>The integral result of the division.</returns>
        public static Integer DivRem(this Integer left, Integer right,
                                     out Integer remainder)
        {
            if (right.length == 0)
                throw new DivideByZeroException();
            if (left.length < right.length)
            {
                remainder = left;
                return new Integer(null, 0, false);
            }

            var remain = default(uint[]);
            var result = Calc.Divide(left.bits, left.length,
                right.bits, right.length, out remain);
            remainder = new Integer(remain, remain.Length, left.sign);
            return new Integer(result, result.Length, left.sign ^ right.sign);
        }
    }
}
