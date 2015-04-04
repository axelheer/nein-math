using System;
using System.Numerics;

namespace NeinMath.Benchmark
{
    public static class Program
    {
        private static int bits = 4096;
        private static string op = "mul";

        public static void Main(string[] args)
        {
            SpeedBoost();
            SanityCheck();

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

        private static void ParseOp(string[] args)
        {
            if (args.Length > 0)
                op = args[0].ToLowerInvariant();

            Console.WriteLine("Operation: {0}", op);
        }

        private static void ParseBits(string[] args)
        {
            var a = ParseIntArg(args, 1);
            if (a.HasValue)
                bits = Math.Max(128, a.Value);

            Console.WriteLine("# of bits: {0}", bits);
        }

        private static void ParseRunCount(string[] args)
        {
            var a = ParseIntArg(args, 2);
            if (a.HasValue)
                Benchmark.RunCount = Math.Max(4, a.Value);

            Console.WriteLine("# of runs: {0}", Benchmark.RunCount);
        }

        private static void ParseValCount(string[] args)
        {
            var a = ParseIntArg(args, 3);
            if (a.HasValue)
                Benchmark.ValCount = Math.Max(1, a.Value);

            Console.WriteLine("# of vals: {0}", Benchmark.ValCount);
        }

        private static int? ParseIntArg(string[] args, int index)
        {
            if (args.Length > index)
            {
                var a = 0;
                if (int.TryParse(args[index], out a))
                    return a;
            }
            return null;
        }

        private static void SpeedBoost()
        {
#if !DEBUG
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass
                = System.Diagnostics.ProcessPriorityClass.High;
            System.Threading.Thread.CurrentThread.Priority
                = System.Threading.ThreadPriority.Highest;
#endif
        }

        private static void SanityCheck()
        {
#if DEBUG
            Console.WriteLine("WARNING: you're using a debug build...");
            Console.WriteLine("...so don't take the results for real.");
            Console.WriteLine();
#endif
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("WARNING: you're debugging this junk...");
                Console.WriteLine("...so don't take the results for real.");
                Console.WriteLine();
            }
        }
    }
}
