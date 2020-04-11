NeinMath
========

[![Latest package](https://img.shields.io/nuget/v/NeinMath.svg)](https://www.nuget.org/packages/NeinMath)
[![Download tracker](https://img.shields.io/nuget/dt/NeinMath.svg)](https://www.nuget.org/packages/NeinMath)
[![GitHub status](https://github.com/axelheer/nein-math/workflows/everything/badge.svg)](https://ci.appveyor.com/project/axelheer/nein-math/branch/master)
[![Code coverage](https://codecov.io/gh/axelheer/nein-math/branch/master/graph/badge.svg)](https://codecov.io/gh/axelheer/nein-math)

*NeinMath* is playing around with arbitrary precision integers, written in pure managed code, not using any unsafe stuff, and a bit faster than the build-in .NET type for integers with a few thousand bits.

To install *NeinMath*, run the following command in the [NuGet Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

    PM> Install-Package NeinMath

It's generally based on the integer implementation of [this work][0], but rewritten to not use pointer arithmetic and other fancy things. Thus, it's a bit slower albeit portable.

*Note:* starting with the new .NET Core this project becomes a bit obsolete, because the performance gains disappear since some improvements have been [contributed][2]. :tada:

Performance
-----------

Let's start with a simple comparison (time per 100 operations).

| Operation | Length (bits) | BigInteger (.NET) | Integer (NeinMath) |
|:----------|--------------:|------------------:|-------------------:|
| log       |     4,194,304 |           1306 ms |               0 ms |
| add (+)   |     4,194,304 |             40 ms |              17 ms |
| sub (-)   |     4,194,304 |             43 ms |              18 ms |
| mul (*)   |        65,536 |            980 ms |             116 ms |
| squ (^2)  |        65,536 |            980 ms |              82 ms |
| div (/)   |        65,536 |            555 ms |             231 ms |
| mod (%)   |        65,536 |            555 ms |             231 ms |
| gcd       |        65,536 |            730 ms |             532 ms |
| modinv    |        65,536 |               N/A |           1,412 ms |
| modpow    |        16,384 |      5,124,600 ms |         652,900 ms |

*Note:* ensure you're running a 64-bit process. Handling this with just 32-bits is a huge impediment for both, `BigInteger` and `Integer`.

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

*Note:* calling `left.Gcd(right)` is much faster, since the internal implementation is based on a more sophisticated algorithm.

Rationals
---------

Coming sometime... maybe... who knows?


[0]: http://axel.heer.eu/2011/02/05/big-integer-arithmetik/
[1]: http://msdn.microsoft.com/library/system.numerics.biginteger
[2]: http://github.com/dotnet/corefx/issues/1307
