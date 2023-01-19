namespace NeinMath
{
    /// <remarks>
    /// Helper methods to keep all the bits in rein.
    /// </remarks>
    static class Bits
    {
        public static int Length(uint[] value, int length)
        {
            while (length > 0 && value[length - 1] == 0)
                --length;

            return length;
        }

        public static int LeadingZeros(uint value)
        {
            if (value == 0)
                return 32;

            var count = 0;
            if ((value & 0xFFFF0000) == 0)
            {
                count += 16;
                value = value << 16;
            }
            if ((value & 0xFF000000) == 0)
            {
                count += 8;
                value = value << 8;
            }
            if ((value & 0xF0000000) == 0)
            {
                count += 4;
                value = value << 4;
            }
            if ((value & 0xC0000000) == 0)
            {
                count += 2;
                value = value << 2;
            }
            if ((value & 0x80000000) == 0)
            {
                count += 1;
            }

            return count;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint right)
        {
            if (leftLength == 0)
            {
                if (right == 0)
                    return 0;
                return -1;
            }

            if (leftLength > 1)
                return 1;

            if (left[0] < right)
                return -1;
            if (left[0] > right)
                return 1;

            return 0;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint[] right, int rightLength)
        {
            if (leftLength < rightLength)
                return -1;
            if (leftLength > rightLength)
                return 1;

            for (var i = leftLength - 1; i >= 0; i--)
            {
                if (left[i] < right[i])
                    return -1;
                if (left[i] > right[i])
                    return 1;
            }

            return 0;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint[] right, int rightLength,
                                  int rightOffset)
        {
            if (leftLength < rightLength + rightOffset)
                return -1;
            if (leftLength > rightLength + rightOffset)
                return 1;

            for (var i = leftLength - 1; i >= rightOffset; i--)
            {
                if (left[i] < right[i - rightOffset])
                    return -1;
                if (left[i] > right[i - rightOffset])
                    return 1;
            }
            for (var i = rightOffset - 1; i >= 0; i--)
            {
                if (left[i] < 0)
                    return -1;
                if (left[i] > 0)
                    return 1;
            }

            return 0;
        }

        public static uint[] OnesComplement(uint[] value, int length)
        {
            if (length == 0)
                return new uint[] { 0xFFFFFFFF };

            var result = new uint[length];
            for (var i = 0; i < length; i++)
                result[i] = ~value[i];

            return result;
        }

        public static uint[] TwosComplement(uint[] value, int length)
        {
            var result = new uint[length];

            var carry = 1UL;
            for (var i = 0; i < length; i++)
            {
                var digit = (uint)~value[i] + carry;
                result[i] = (uint)digit;
                carry = digit >> 32;
            }
            if (carry != 0)
            {
                result = new uint[length + 1];
                result[length] = 1;
            }

            return result;
        }

        public static byte[] TwosComplement(byte[] value, int length)
        {
            var result = new byte[length];

            var carry = 1U;
            for (var i = 0; i < length; i++)
            {
                var digit = (byte)~value[i] + carry;
                result[i] = (byte)digit;
                carry = digit >> 8;
            }
            if (carry != 0)
            {
                result = new byte[length + 1];
                result[length] = 1;
            }

            return result;
        }

        public static ulong Abs(long value)
        {
            var mask = (ulong)(value >> 63);
            return ((ulong)value ^ mask) - mask;
        }

        public static uint Abs(int value)
        {
            var mask = (uint)(value >> 31);
            return ((uint)value ^ mask) - mask;
        }
    }
}
