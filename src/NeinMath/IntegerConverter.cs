using System;
using System.Globalization;

namespace NeinMath
{
    /// <summary>
    /// Converts Integers to another thing, and some thing to Integers.
    /// </summary>
    public static class IntegerConverter
    {
        /// <summary>
        /// Returns the specified Integer as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] ToByteArray(this Integer value)
        {
            if (value.length == 0)
                return new byte[] { 0 };

            var bits = value.length * 32
                - Bits.LeadingZeros(value.bits[value.length - 1]) + 1;
            var bytes = new byte[(bits + 7) / 8];
            for (var i = 0; i < (bits + 6) / 8; i++)
            {
                var shift = (i % 4) * 8;
                bytes[i] = (byte)(value.bits[i / 4] >> shift);
            }
            if (value.sign)
                bytes = Bits.TwosComplement(bytes, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Returns an Integer converted from an array of bytes.
        /// </summary>
        /// <param name="value">An array of bytes.</param>
        /// <returns>An Integer formed by an array of bytes.</returns>
        public static Integer FromByteArray(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var sign = value.Length != 0
                && (value[value.Length - 1] & 0x80) != 0;
            if (sign)
                value = Bits.TwosComplement(value, value.Length);
            var bits = new uint[(value.Length + 3) / 4];
            for (var i = 0; i < value.Length; i++)
            {
                var shift = (i % 4) * 8;
                bits[i / 4] |= (uint)(value[i] << shift);
            }

            return new Integer(bits, bits.Length, sign);
        }

        /// <summary>
        /// Returns the specified Integer as a hex string.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A hex string.</returns>
        public static string ToHexString(this Integer value)
        {
            var bytes = ToByteArray(value);

            var digits = new string[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
                digits[i] = bytes[bytes.Length - i - 1].ToString("X2",
                    CultureInfo.InvariantCulture);
            var trivialSign = (digits[0][0] == '0' && digits[0][1] < '8')
                || (digits[0][0] == 'F' && digits[0][1] >= '8');
            if (trivialSign)
                digits[0] = digits[0].Substring(1);

            return string.Concat(digits);
        }

        /// <summary>
        /// Returns an Integer converted from a hex string.
        /// </summary>
        /// <param name="value">A hex string.</param>
        /// <returns>An Integer formed by a hex string.</returns>
        public static Integer FromHexString(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            var sign = value[0] >= '8' ? "F" : "0";
            if (value.Length % 2 == 1)
                value = sign + value;
            var bytes = new byte[value.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(value.Substring(value.Length
                    - (i + 1) * 2, 2), NumberStyles.AllowHexSpecifier,
                    CultureInfo.InvariantCulture);

            return FromByteArray(bytes);
        }

        /// <summary>
        /// Returns the specified Integer as a decimal string.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A decimal string.</returns>
        public static string ToDecimalString(this Integer value)
        {
            if (value.length == 0)
                return "0";

            var result = "";
            var bits = value.bits;
            var length = value.length;
            while (length > 0)
            {
                var r = 0U;
                bits = Calc.Divide(bits, length, 1000000000U, out r);
                result = r.ToString("000000000", CultureInfo.InvariantCulture)
                    + result;
                length = Bits.Length(bits, length);
            }
            result = result.TrimStart('0');
            if (value.sign)
                result = '-' + result;

            return result;
        }

        /// <summary>
        /// Returns an Integer converted from a decimal string.
        /// </summary>
        /// <param name="value">A decimal string.</param>
        /// <returns>An Integer formed by a decimal string.</returns>
        public static Integer FromDecimalString(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (value.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            var sign = value[0] == '-';
            if (sign)
                value = value.Substring(1);
            var result = default(uint[]);
            var resultLength = 0;
            while (value.Length > 0)
            {
                var length = value.Length < 9 ? value.Length : 9;
                var chunk = value.Substring(0, length);
                result = Calc.Multiply(result, resultLength,
                    (uint)Math.Pow(10, length));
                resultLength = Bits.Length(result, result.Length);
                result = Calc.Add(result, resultLength,
                    uint.Parse(chunk, NumberStyles.None,
                    CultureInfo.InvariantCulture));
                resultLength = Bits.Length(result, result.Length);
                value = value.Substring(length);
            }

            return new Integer(result, resultLength, sign);
        }
    }
}
