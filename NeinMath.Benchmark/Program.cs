using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

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

                case "modpow":
                    Benchmark.RunCount = 5;
                    Benchmark.ValCount = 1;
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
            if (args.Length > 1)
            {
                var a = 0;
                if (int.TryParse(args[1], out a))
                    bits = Math.Max(128, a);
            }

            Console.WriteLine("Length: {0} bits", bits);
        }

        private static void SpeedBoost()
        {
#if !DEBUG
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
#endif
        }

        private static void SanityCheck()
        {
#if DEBUG
            Console.WriteLine("WARNING: you're using a debug build...");
            Console.WriteLine("...so don't take the results for real.");
            Console.WriteLine();
#endif
            if (Debugger.IsAttached)
            {
                Console.WriteLine("WARNING: you're debugging this junk...");
                Console.WriteLine("...so don't take the results for real.");
                Console.WriteLine();
            }
        }
    }
}
