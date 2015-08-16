namespace NeinMath
{
    static class Lehmer
    {
        public static Integer Gcd(Integer x, Integer y)
        {
            // retrieve private copy (!)
            var xBits = Calc.Shift(x.bits, x.length, 0, 0);
            var yBits = Calc.Shift(y.bits, y.length, 0, 0);

            var result = Gcd(xBits, x.length, yBits, y.length);
            return new Integer(result, result.Length, false);
        }

        public static Integer Inv(Integer x, Integer y)
        {
            // retrieve private copy (!)
            var xBits = Calc.Shift(x.bits, x.length, 0, 0);
            var yBits = Calc.Shift(y.bits, y.length, 0, 0);

            var result = Inv(xBits, x.length, yBits, y.length);
            if (result[result.Length - 1] != 0)
            {
                result = Bits.TwosComplement(result, result.Length);
                return new Integer(result, result.Length, true);
            }
            return new Integer(result, result.Length, false);
        }

        static uint[] Gcd(uint[] xBits, int xLength,
                          uint[] yBits, int yLength)
        {
            while (yLength > 1)
            {
                var x = 0L;
                var y = 0L;

                // extract most significant bits
                Values(xBits, xLength, yBits, yLength, out x, out y);

                var a = 1L; var b = 0L;
                var c = 0L; var d = 1L;

                // Lehmer's guessing
                while (y + c != 0 && y + d != 0)
                {
                    var q = (x + a) / (y + c);
                    var qc = (x + b) / (y + d);

                    if (q != qc)
                        break;

                    var r = a - q * c;
                    var s = b - q * d;
                    var t = x - q * y;

                    a = c; c = r;
                    b = d; d = s;
                    x = y; y = t;
                }

                if (b == 0)
                {
                    // Euclid's step
                    var tBits = default(uint[]);
                    Calc.Divide(xBits, xLength, yBits, yLength,
                        out tBits);

                    xBits = yBits; xLength = yLength;
                    yBits = tBits; yLength = Bits.Length(tBits, tBits.Length);
                }
                else
                {
                    // Lehmer's step
                    var length = yLength;
                    MulAdd(a, b, c, d, xBits, yBits, length);

                    xLength = Bits.Length(xBits, length);
                    yLength = Bits.Length(yBits, length);
                }
            }

            // ordinary algorithm
            if (yLength == 1)
            {
                // Euclid's step
                var y = Calc.Remainder(xBits, xLength, yBits[0]);
                var x = yBits[0];

                while (y != 0)
                {
                    // Euclid's step
                    var t = x % y;
                    x = y;
                    y = t;
                }

                return new uint[] { x };
            }

            // trim it!
            return Calc.Shift(xBits, xLength, 0, 0);
        }

        static uint[] Inv(uint[] xBits, int xLength,
                          uint[] yBits, int yLength)
        {
            // reserve one digit for the sign!
            var iBits = new uint[xLength + 1];
            var jBits = new uint[xLength + 1];
            jBits[0] = 1;

            while (yLength > 1)
            {
                var x = 0L;
                var y = 0L;

                // extract most significant bits
                Values(xBits, xLength, yBits, yLength, out x, out y);

                var a = 1L; var b = 0L;
                var c = 0L; var d = 1L;

                // Lehmer's guessing
                while (y + c != 0 && y + d != 0)
                {
                    var q = (x + a) / (y + c);
                    var qc = (x + b) / (y + d);

                    if (q != qc)
                        break;

                    var r = a - q * c;
                    var s = b - q * d;
                    var t = x - q * y;

                    a = c; c = r;
                    b = d; d = s;
                    x = y; y = t;
                }

                if (b == 0)
                {
                    // Euclid's step
                    var tBits = default(uint[]);
                    var qBits = Calc.Divide(xBits, xLength, yBits, yLength,
                        out tBits);

                    xBits = yBits; xLength = yLength;
                    yBits = tBits; yLength = Bits.Length(tBits, tBits.Length);

                    // Enhanced Euclid's step
                    var uBits = iBits;
                    var vBits = Calc.Multiply(jBits, jBits.Length,
                                              qBits, qBits.Length);
                    Calc.SubtractSelf(iBits, iBits.Length,
                                      vBits, iBits.Length);
                    iBits = jBits; jBits = uBits;
                }
                else
                {
                    // Lehmer's step
                    var length = yLength;
                    MulAdd(a, b, c, d, xBits, yBits, length);

                    xLength = Bits.Length(xBits, length);
                    yLength = Bits.Length(yBits, length);

                    // Enhanced Lehmer's step
                    MulAdd(a, b, c, d, iBits, jBits, jBits.Length);
                }
            }

            // ordinary algorithm
            if (yLength == 1)
            {
                // Euclid's step
                var y = default(uint);
                var qBits = Calc.Divide(xBits, xLength, yBits[0], out y);
                var x = yBits[0];

                // Enhanced Euclid's step
                var uBits = iBits;
                var vBits = Calc.Multiply(jBits, jBits.Length,
                                          qBits, qBits.Length);
                Calc.SubtractSelf(iBits, iBits.Length,
                                  vBits, iBits.Length);
                iBits = jBits; jBits = uBits;

                while (y != 0)
                {
                    // Euclid's step
                    var q = x / y;
                    var t = x % y;
                    x = y;
                    y = t;

                    // Enhanced Euclid's step
                    uBits = iBits;
                    vBits = Calc.Multiply(jBits, jBits.Length, q);
                    Calc.SubtractSelf(iBits, iBits.Length,
                                      vBits, iBits.Length);
                    iBits = jBits; jBits = uBits;
                }

                if (x == 1)
                    return iBits;
            }

            return new uint[] { 0 };
        }

        static void Values(uint[] xBits, int xLength,
                           uint[] yBits, int yLength,
                           out long x, out long y)
        {
            var xh = 0UL; var xm = 0UL; var xl = 0UL;
            var yh = 0UL; var ym = 0UL; var yl = 0UL;

            // has low digit?
            var hasLo = xLength > 2;

            xh = xBits[xLength - 1];
            xm = xBits[xLength - 2];
            if (hasLo) xl = xBits[xLength - 3];

            // arrange the bits
            switch (xLength - yLength)
            {
                case 0:
                    yh = yBits[yLength - 1];
                    ym = yBits[yLength - 2];
                    if (hasLo) yl = yBits[yLength - 3];
                    break;

                case 1:
                    ym = yBits[yLength - 1];
                    if (hasLo) yl = yBits[yLength - 2];
                    break;

                case 2:
                    if (hasLo) yl = yBits[yLength - 1];
                    break;
            }

            // use all the bits but two
            var z = Bits.LeadingZeros((uint)xh);

            x = (long)(((xh << 32 + z) | (xm << z) | (xl >> 32 - z)) >> 2);
            y = (long)(((yh << 32 + z) | (ym << z) | (yl >> 32 - z)) >> 2);
        }

        static void MulAdd(long a, long b, long c, long d,
                           uint[] x, uint[] y, int length)
        {
            var xCarry = 0L;
            var yCarry = 0L;
            for (var i = 0; i < length; i++)
            {
                var xDigits = a * x[i] + b * y[i] + xCarry;
                var yDigits = c * x[i] + d * y[i] + yCarry;
                x[i] = (uint)xDigits;
                y[i] = (uint)yDigits;
                xCarry = xDigits >> 32;
                yCarry = yDigits >> 32;
            }
        }
    }
}
