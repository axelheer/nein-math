namespace NeinMath
{
    /// <remarks>
    /// Integer arrays may be null or bigger than necessary, appropriate length
    /// parameters must be used. The left parameter, if any, is always assumed
    /// as the more lengthy one. No checks, beware!
    /// </remarks>
    static class Calc
    {
        public static uint[] And(uint[] left, int leftLength,
                                 uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { };

            var bits = new uint[leftLength];
            bits[0] = left[0] & right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] & rightPad;

            return bits;
        }

        public static uint[] And(uint[] left, int leftLength,
                                 uint[] right, int rightLength,
                                 uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] & right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] & rightPad;

            return bits;
        }

        public static uint[] Or(uint[] left, int leftLength,
                                uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength];
            bits[0] = left[0] | right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] | rightPad;

            return bits;
        }

        public static uint[] Or(uint[] left, int leftLength,
                                uint[] right, int rightLength,
                                uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] | right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] | rightPad;

            return bits;
        }

        public static uint[] Xor(uint[] left, int leftLength,
                                 uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength];
            bits[0] = left[0] ^ right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] ^ rightPad;

            return bits;
        }

        public static uint[] Xor(uint[] left, int leftLength,
                                 uint[] right, int rightLength,
                                 uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] ^ right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] ^ rightPad;

            return bits;
        }

        public static uint[] Shift(uint[] value, int length,
                                   int shift, uint pad)
        {
            if (length == 0)
                return new uint[] { pad };

            if (shift < 0)
            {
                // big shifts move entire blocks
                var leapShift = -shift / 32;
                if (length <= leapShift)
                    return new uint[] { pad };
                var tinyShift = -shift % 32;

                // shifts the bits to the right
                var bits = new uint[length - leapShift];
                if (tinyShift == 0)
                {
                    for (var i = 0; i < bits.Length; i++)
                        bits[i] = value[i + leapShift];
                }
                else
                {
                    for (var i = 0; i < bits.Length - 1; i++)
                        bits[i] = (value[i + leapShift] >> tinyShift)
                            | (value[i + leapShift + 1] << (32 - tinyShift));
                    bits[bits.Length - 1] = (pad << (32 - tinyShift))
                        | (value[length - 1] >> tinyShift);
                }

                return bits;
            }
            if (shift > 0)
            {
                // big shifts move entire blocks
                var leapShift = shift / 32;
                var tinyShift = shift % 32;

                // shifts the bits to the left
                var bits = new uint[length + leapShift + 1];
                if (tinyShift == 0)
                {
                    for (var i = leapShift; i < bits.Length - 1; i++)
                        bits[i] = value[i - leapShift];
                    bits[bits.Length - 1] = pad;
                }
                else
                {
                    for (var i = leapShift + 1; i < bits.Length - 1; i++)
                    {
                        bits[i] = (value[i - leapShift] << tinyShift)
                            | (value[i - leapShift - 1] >> (32 - tinyShift));
                    }
                    bits[leapShift] = value[0] << tinyShift;
                    bits[bits.Length - 1] = (pad << tinyShift)
                        | (value[length - 1] >> (32 - tinyShift));
                }

                return bits;
            }
            else
            {
                // no shift at all...
                var bits = new uint[length];
                for (var i = 0; i < length; i++)
                    bits[i] = value[i];

                return bits;
            }
        }

        public static uint[] Add(uint[] left, int leftLength,
                                 uint right)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength + 1];

            // first operation
            var digit = (long)left[0] + right;
            bits[0] = (uint)digit;
            var carry = digit >> 32;

            // adds the bits
            for (var i = 1; i < leftLength; i++)
            {
                digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static uint[] Add(uint[] left, int leftLength,
                                 uint[] right, int rightLength)
        {
            var bits = new uint[leftLength + 1];
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) + right[i];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static void AddSelf(uint[] left, int leftLength,
                                   uint[] right, int rightLength)
        {
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) + right[i];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static void AddSelf(uint[] left, int leftLength,
                                   uint[] right, int rightLength,
                                   int rightOffset)
        {
            var bound = rightOffset + rightLength;
            var carry = 0L;

            // adds the bits
            for (var i = rightOffset; i < bound; i++)
            {
                var digit = (left[i] + carry) + right[i - rightOffset];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = bound; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        static uint[] AddFold(uint[] value,
                              int leftLength, int leftOffset,
                              int rightLength, int rightOffset)
        {
            var bits = new uint[leftLength + 1];
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (value[i + leftOffset] + carry)
                    + value[i + rightOffset];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = value[i + leftOffset] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static uint[] Subtract(uint[] left, int leftLength,
                                      uint right)
        {
            if (leftLength == 0)
                return new uint[] { };

            var bits = new uint[leftLength];

            // first operation
            var digit = (long)left[0] - right;
            bits[0] = (uint)digit;
            var carry = digit >> 32;

            // subtracts the bits
            for (var i = 1; i < leftLength; i++)
            {
                digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }

            return bits;
        }

        public static uint[] Subtract(uint[] left, int leftLength,
                                      uint[] right, int rightLength)
        {
            var bits = new uint[leftLength];
            var carry = 0L;

            // subtracts the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) - right[i];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }

            return bits;
        }

        public static void SubtractSelf(uint[] left, int leftLength,
                                        uint[] right, int rightLength)
        {
            var carry = 0L;

            // subtract the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) - right[i];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static void SubtractSelf(uint[] left, int leftLength,
                                        uint[] right, int rightLength,
                                        int rightOffset)
        {
            var bound = rightOffset + rightLength;
            var carry = 0L;

            // subtract the bits
            for (var i = rightOffset; i < bound; i++)
            {
                var digit = (left[i] + carry) - right[i - rightOffset];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = bound; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        static void SubtractCore(uint[] value,
                                 int leftLength, int leftOffset,
                                 int rightLength, int rightOffset,
                                 uint[] core, int coreLength)
        {
            var carry = 0L;

            // subtract the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (core[i] + carry)
                    - value[i + leftOffset]
                    - value[i + rightOffset];
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = (core[i] + carry)
                    - value[i + leftOffset];
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = leftLength; carry != 0 && i < coreLength; i++)
            {
                var digit = core[i] + carry;
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static uint[] Square(uint[] value, int valueLength)
        {
            return Square(value, valueLength, 0);
        }

#if DEBUG

        const int SquareThreshold = 128 / 32;

#else

        const int SquareThreshold = 1024 / 32;

#endif

        public static uint[] Square(uint[] value, int valueLength,
                                    int valueOffset)
        {
            var bits = new uint[valueLength * 2];

            Square(value, valueLength, valueOffset, bits, 0);

            return bits;
        }

        static void Square(uint[] value, int valueLength,
                           int valueOffset,
                           uint[] bits, int bitsOffset)
        {
            if (valueLength < SquareThreshold)
            {
                // squares the bits
                for (var i = 0; i < valueLength; i++)
                {
                    var carry = 0UL;
                    for (var j = 0; j < i; j++)
                    {
                        var digit1 = bits[i + j + bitsOffset] + carry;
                        var digit2 = (ulong)value[j + valueOffset]
                            * (ulong)value[i + valueOffset];
                        bits[i + j + bitsOffset] =
                            (uint)(digit1 + (digit2 << 1));
                        carry = (digit2 + (digit1 >> 1)) >> 31;
                    }
                    var digit = (ulong)value[i + valueOffset]
                        * (ulong)value[i + valueOffset] + carry;
                    bits[i * 2 + bitsOffset] = (uint)digit;
                    bits[i * 2 + 1 + bitsOffset] = (uint)(digit >> 32);
                }
            }
            else
            {
                // divide & conquer
                var n = valueLength / 2;

                var lowOffset = valueOffset;
                var lowLength = n;
                var highOffset = valueOffset + n;
                var highLength = valueLength - n;

                Square(value, lowLength, lowOffset, bits, bitsOffset);
                Square(value, highLength, highOffset, bits, bitsOffset + n * 2);

                var fold = AddFold(value, highLength, highOffset,
                                          lowLength, lowOffset);
                var foldLength = fold[fold.Length - 1] == 0
                               ? fold.Length - 1 : fold.Length;

                var core = Square(fold, foldLength);
                SubtractCore(bits, highLength * 2, bitsOffset + n * 2,
                                   lowLength * 2, bitsOffset,
                                   core, core.Length);

                // merge the result
                AddSelf(bits, bits.Length, core, core.Length, bitsOffset + n);
            }
        }

        public static uint[] Multiply(uint[] left, int leftLength,
                                      uint right)
        {
            var bits = new uint[leftLength + 1];

            // multiplies the bits
            var carry = 0UL;
            for (var j = 0; j < leftLength; j++)
            {
                var digits = (ulong)left[j] * right + carry;
                bits[j] = (uint)digits;
                carry = digits >> 32;
            }
            bits[leftLength] = (uint)carry;

            return bits;
        }

        public static uint[] Multiply(uint[] left, int leftLength,
                                      uint[] right, int rightLength)
        {
            return Multiply(left, leftLength, 0, right, rightLength, 0);
        }

#if DEBUG

        const int MultiplyThreshold = 128 / 32;

#else

        const int MultiplyThreshold = 1024 / 32;

#endif

        public static uint[] Multiply(uint[] left, int leftLength,
                                      int leftOffset,
                                      uint[] right, int rightLength,
                                      int rightOffset)
        {
            var bits = new uint[leftLength + rightLength];

            Multiply(left, leftLength, leftOffset,
                     right, rightLength, rightOffset,
                     bits, 0);

            return bits;
        }

        static void Multiply(uint[] left, int leftLength,
                             int leftOffset,
                             uint[] right, int rightLength,
                             int rightOffset,
                             uint[] bits, int bitsOffset)
        {
            if (leftLength < MultiplyThreshold
                || rightLength < MultiplyThreshold)
            {
                // multiplies the bits
                for (var i = 0; i < rightLength; i++)
                {
                    var carry = 0UL;
                    for (var j = 0; j < leftLength; j++)
                    {
                        var digits = bits[i + j + bitsOffset] + carry
                            + (ulong)left[j + leftOffset]
                            * (ulong)right[i + rightOffset];
                        bits[i + j + bitsOffset] = (uint)digits;
                        carry = digits >> 32;
                    }
                    bits[i + leftLength + bitsOffset] = (uint)carry;
                }
            }
            else
            {
                // divide & conquer
                var n = (leftLength < rightLength
                      ? leftLength : rightLength) / 2;

                // x = (x_1 << n) + x_0
                var leftLowOffset = leftOffset;
                var leftLowLength = n;
                var leftHighOffset = leftOffset + n;
                var leftHighLength = leftLength - n;

                // y = (y_1 << n) + y_0
                var rightLowOffset = rightOffset;
                var rightLowLength = n;
                var rightHighOffset = rightOffset + n;
                var rightHighLength = rightLength - n;

                // z_0 = x_0 * y_0
                Multiply(left, leftLowLength, leftLowOffset,
                         right, rightLowLength, rightLowOffset,
                         bits, bitsOffset);

                // z_2 = x_1 * y_1
                Multiply(left, leftHighLength, leftHighOffset,
                         right, rightHighLength, rightHighOffset,
                         bits, bitsOffset + n * 2);

                // z_x = x_1 + x_0
                var leftFold = AddFold(left, leftHighLength, leftHighOffset,
                                       leftLowLength, leftLowOffset);
                var leftFoldLength = leftFold[leftFold.Length - 1] == 0
                                   ? leftFold.Length - 1 : leftFold.Length;

                // z_y = y_1 + y_0
                var rightFold = AddFold(right, rightHighLength, rightHighOffset,
                                        rightLowLength, rightLowOffset);
                var rightFoldLength = rightFold[rightFold.Length - 1] == 0
                                    ? rightFold.Length - 1 : rightFold.Length;

                // z_1 = z_x * z_y - z_0 - z_2
                var core = Multiply(leftFold, leftFoldLength,
                                    rightFold, rightFoldLength);
                SubtractCore(bits,
                    leftHighLength + rightHighLength, bitsOffset + n * 2,
                    leftLowLength + rightLowLength, bitsOffset,
                    core, core.Length);

                // merge the result
                AddSelf(bits, bits.Length, core, core.Length, bitsOffset + n);
            }
        }

        public static uint[] Divide(uint[] left, int leftLength,
                                    uint right, out uint remainder)
        {
            var bits = new uint[leftLength];

            // divides the bits
            var carry = 0UL;
            for (var i = leftLength - 1; i >= 0; i--)
            {
                var value = (carry << 32) | left[i];
                bits[i] = (uint)(value / right);
                carry = value % right;
            }
            remainder = (uint)carry;

            return bits;
        }

        public static uint Remainder(uint[] left, int leftLength,
                                     uint right)
        {
            // divides the bits
            var carry = 0UL;
            for (var i = leftLength - 1; i >= 0; i--)
            {
                var value = (carry << 32) | left[i];
                carry = value % right;
            }

            return (uint)carry;
        }

        public static uint[] Divide(uint[] left, int leftLength,
                                    uint[] right, int rightLength,
                                    out uint[] remainder)
        {
           var bits = new uint[leftLength - rightLength + 1];

           // get more bits into the highest bit block
           var shifted = Bits.LeadingZeros(right[rightLength - 1]);
           left = Shift(left, leftLength, shifted, 0);
           right = Shift(right, rightLength, shifted, 0);

           // measure again (after shift...)
           leftLength = left[left.Length - 1] == 0
               ? left.Length - 1 : left.Length;

           // these values are useful
           var divHi = right[rightLength - 1];
           var divLo = rightLength > 1 ? right[rightLength - 2] : 0;
           var guess = new uint[rightLength + 1];
           var guessLength = 0;
           var delta = 0;

           // sub the divisor
           do
           {
               delta = Bits.Compare(left, leftLength,
                   right, rightLength, leftLength - rightLength);
               if (delta >= 0)
               {
                   ++bits[leftLength - rightLength];
                   SubtractSelf(left, leftLength,
                       right, rightLength, leftLength - rightLength);
               }
           }
           while (delta > 0);

           // divides the rest of the bits
           for (var i = leftLength - 1; i >= rightLength; i--)
           {
               // first guess for the current bit of the quotient
               var leftHi = (left[i - 1] | ((ulong)left[i] << 32));
               var digits = leftHi / divHi;
               if (digits > 0xFFFFFFFF)
                   digits = 0xFFFFFFFF;

               // the guess may be a little bit to big
               var check = divHi * digits + ((divLo * digits) >> 32);
               if (check > leftHi)
                   --digits;

               // the guess may be still a little bit to big
               do
               {
                   MultiplyDivisor(right, rightLength, digits, guess);
                   guessLength = guess[guess.Length - 1] == 0
                       ? guess.Length - 1 : guess.Length;
                   delta = Bits.Compare(left, left[i] != 0 ? i + 1 : i,
                       guess, guessLength, i - rightLength);
                   if (delta < 0)
                       --digits;
               }
               while (delta < 0);

               // we have the bit!
               SubtractSelf(left, i + 1,
                   guess, guessLength, i - rightLength);
               bits[i - rightLength] = (uint)digits;
           }

           // repair the cheated shift
           remainder = Shift(left, rightLength, -shifted, 0);

           return bits;
        }

        static void MultiplyDivisor(uint[] left, int leftLength,
                                    ulong right, uint[] bits)
        {
            // multiplies the bits
            var carry = 0UL;
            for (var j = 0; j < leftLength; j++)
            {
                var digits = left[j] * right + carry;
                bits[j] = (uint)digits;
                carry = digits >> 32;
            }
            bits[leftLength] = (uint)carry;
        }
    }
}
