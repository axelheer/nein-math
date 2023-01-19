using System;
using System.Numerics;

#pragma warning disable CA1062
#pragma warning disable CA1303
#pragma warning disable CA1308

namespace NeinMath.Benchmark
{
    public static class Program
    {
        static int bits = 4096;
        static string op = "mul";

        public static void Main(string[] args)
        {
            ParseOp(args);
            ParseBits(args);
            ParseRunCount(args);
            ParseValCount(args);

            switch (op)
            {
                case "add":
                    Benchmark.Run(bits, bits,
                        (a, b) => a + b,
                        (a, b) => a + b);
                    break;

                case "sub":
                    Benchmark.Run(bits, bits - 64,
                        (a, b) => a - b,
                        (a, b) => a - b);
                    break;

                case "mul":
                    Benchmark.Run(bits, bits,
                        (a, b) => a * b,
                        (a, b) => a * b);
                    break;

                case "squ":
                    Benchmark.Run(bits,
                        a => a * a,
                        a => a * a);
                    break;

                case "div":
                    Benchmark.Run(bits, bits / 2,
                        (a, b) => a / b,
                        (a, b) => a / b);
                    break;

                case "mod":
                    Benchmark.Run(bits, bits / 2,
                        (a, b) => a % b,
                        (a, b) => a % b);
                    break;

                case "gcd":
                    Benchmark.Run(bits, bits,
                        (a, b) => BigInteger.GreatestCommonDivisor(a, b),
                        (a, b) => a.Gcd(b));
                    break;

                case "modinv":
                    Benchmark.Run(bits, bits,
                        (a, b) => 0, // :'(
                        (a, b) => a.ModInv(b));
                    break;

                case "log":
                    Benchmark.Run(bits,
                        a => BigInteger.Log(a),
                        a => a.Log());
                    break;

                case "modpow":
                    Benchmark.Run(bits, bits, bits,
                        (a, b, c) => BigInteger.ModPow(a, b, c),
                        (a, b, c) => a.ModPow(b, c));
                    break;
            }
        }

        static void ParseOp(string[] args)
        {
            if (args.Length > 0)
                op = args[0].ToLowerInvariant();

            Console.WriteLine("Operation: {0}", op);
        }

        static void ParseBits(string[] args)
        {
            var a = ParseIntArg(args, 1);
            if (a.HasValue)
                bits = Math.Max(128, a.Value);

            Console.WriteLine("# of bits: {0}", bits);
        }

        static void ParseRunCount(string[] args)
        {
            var a = ParseIntArg(args, 2);
            if (a.HasValue)
                Benchmark.RunCount = Math.Max(4, a.Value);

            Console.WriteLine("# of runs: {0}", Benchmark.RunCount);
        }

        static void ParseValCount(string[] args)
        {
            var a = ParseIntArg(args, 3);
            if (a.HasValue)
                Benchmark.ValCount = Math.Max(1, a.Value);

            Console.WriteLine("# of vals: {0}", Benchmark.ValCount);
        }

        static int? ParseIntArg(string[] args, int index)
        {
            if (args.Length > index)
            {
                var a = 0;
                if (int.TryParse(args[index], out a))
                    return a;
            }
            return null;
        }
    }
}
