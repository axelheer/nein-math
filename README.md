NeinMath
========

*NeinMath* is playing around with arbitrary precision integers, written in pure managed code, not using any unsafe stuff, and a bit faster than the build-in .NET type for integers with a few thousand bits.

To install *NeinMath*, run the following command in the [NuGet Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

    PM> Install-Package NeinMath

It's generally based on [this work][0], but rewritten to not use pointer arithmetic and other fancy things. Thus, it's a bit slower albeit portable.


Performance
-----------

Let's start with a simple comparison (time per 100 operations).

| Operation | Length (bits) | BigInteger (.NET) | Integer (NeinMath) |
|:----------|--------------:|------------------:|-------------------:|
| add (+)   |     4,194,304 |             51 ms |              43 ms |
| sub (-)   |     4,194,304 |             52 ms |              63 ms |
| mul (*)   |        65,536 |          1,387 ms |             447 ms |
| squ (^2)  |        65,536 |          1,391 ms |             371 ms |
| div (/)   |        65,536 |          1,023 ms |             888 ms |
| mod (%)   |        65,536 |          1,022 ms |             884 ms |
| modpow    |        16,384 |      8,422,300 ms |       2,600,700 ms |

*Note:* not using pointer arithmetic seems to be a huge impediment. Maybe the future compiler platform does the trick?

*Note:* these results are from "my machine". A basic (very basic) benchmark utility is included to verify / disprove them.


Integers
--------

Like [BigInteger][1] a structure `Integer` provides all the operators you would expect from an integer, so it should be quite compatible to existing .NET code. In fact, there are tests based on `BigInteger` to ensure it computes correctly most of the time.

To get an idea, this is an example for calculating the *Greatest Common Divisor*:

```csharp
Integer Gcd(Integer left, Integer right)
{
    var a = left.Abs();
    var b = right.Abs();

    while (b != 0)
    {
        var c = a % b;
        a = b;
        b = c;
    }

    return a;
}
```

Use at your own risk.

Fractions
---------

Coming sometime... maybe... who knows?


[0]: http://axel.heer.eu/2011/02/05/big-integer-arithmetik/
[1]: http://msdn.microsoft.com/library/system.numerics.biginteger
