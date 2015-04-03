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

            while (b != 0)
            {
                var c = a % b;
                a = b;
                b = c;
            }

            return a;
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
        /// Performs modulus inversion on a number.
        /// </summary>
        /// <param name="value">The number to inverse.</param>
        /// <param name="modulus">The number by which to divide.</param>
        /// <returns>The result of inversing the number; if any.</returns>
        public static Integer? Inv(this Integer value, Integer modulus)
        {
            if (modulus < 1)
                throw new ArgumentOutOfRangeException("modulus");

            if (Abs(value) >= modulus)
                value = value % modulus;
            if (value < 0)
                value = value + modulus;
            if (value == 0)
                return null;

            var a = modulus;
            var b = value;

            // Integer x2 = 1, x1 = 0;
            Integer y2 = 0, y1 = 1;

            while (b != 0)
            {
                var q = a / b;
                var r = a - q * b;
                // var x = x2 - q * x1;
                var y = y2 - q * y1;

                a = b; b = r;
                // x2 = x1; x1 = x;
                y2 = y1; y1 = y;
            }

            if (a != 1)
                return null;

            return y2;
        }

        /// <summary>
        /// Raises an Integer to the power of a specified value.
        /// </summary>
        /// <param name="value">
        /// The number to raise to the exponent power.
        /// </param>
        /// <param name="power">The exponent to raise the number by.</param>
        /// <returns>
        /// The result of raising the number to the exponent power.
        /// </returns>
        public static Integer Pow(this Integer value, int power)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException("power");

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
        /// Performs modulus division on a number raised to the power of
        /// another number.
        /// </summary>
        /// <param name="value">
        /// The number to raise to the exponent power.
        /// </param>
        /// <param name="power">The exponent to raise the number by.</param>
        /// <param name="modulus">The number by which to divide.</param>
        /// <returns>
        /// The result of raising the number to the exponent power.
        /// </returns>
        public static Integer ModPow(this Integer value, Integer power,
                                     Integer modulus)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException("power");
            if (modulus < 1)
                throw new ArgumentOutOfRangeException("modulus");

            var R = BeginBarrett(modulus);
            var mu = R / modulus;

            if (Abs(value) > R)
                value = value % modulus;

            var v = new Integer[256];
            v[0] = 1;
            v[1] = Barrett(value, modulus, mu);
            v[2] = Barrett(v[1] * v[1], modulus, mu);

            for (var j = 4; j < v.Length; j *= 2)
                v[j] = Barrett(v[j / 2] * v[j / 2], modulus, mu);
            for (var i = 3; i < v.Length; i += 2)
            {
                v[i] = Barrett(v[i - 1] * v[1], modulus, mu);
                for (var j = i * 2; j < v.Length; j *= 2)
                    v[j] = Barrett(v[j / 2] * v[j / 2], modulus, mu);
            }

            var p = power.ToByteArray();

            Integer result = v[p[p.Length - 1]];
            for (var i = p.Length - 2; i >= 0; i--)
            {
                for (var j = 0; j < 8; j++)
                    result = Barrett(result * result, modulus, mu);
                result = Barrett(result * v[p[i]], modulus, mu);
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

        private static Integer BeginBarrett(Integer modulus)
        {
            var bits = new uint[modulus.length * 2 + 1];
            bits[bits.Length - 1] = 1;
            return new Integer(bits, bits.Length, false);
        }

        private static Integer Barrett(Integer value, Integer modulus,
                                       Integer mu)
        {
            var v = value.bits; var vl = value.length;
            var m = modulus.bits; var ml = modulus.length;
            var u = mu.bits; var ul = mu.length;

            var l1 = vl - ml + 1;
            var q1 = l1 > 0
                ? Calc.Multiply(v, l1, ml - 1, u, ul, 0)
                : new uint[] { };

            var l2 = Bits.Length(q1, q1.Length) - ml - 1;
            var q2 = l2 > 0
                ? Calc.Multiply(q1, l2, ml + 1, m, ml, 0)
                : new uint[] { };

            var q = q2;
            var ql = Bits.Length(q, q.Length);

            var r = Calc.Subtract(v, vl > ml + 1 ? ml + 1 : vl,
                                  q, ql > ml + 1 ? ml + 1 : ql);
            var rl = Bits.Length(r, r.Length);

            while (Bits.Compare(r, rl, m, ml) >= 0)
            {
                Calc.SubtractSelf(r, rl, m, ml);
                rl = Bits.Length(r, r.Length);
            }

            return new Integer(r, rl, value.sign);
        }
    }
}
