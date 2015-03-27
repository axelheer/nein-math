using System;

#if SOMETIME

namespace NeinMath
{
    /// <summary>
    /// Represents an arbitrarily large signed fraction.
    /// </summary>
    public struct Fraction : IComparable,
                             IComparable<int>,
                             IComparable<Integer>, IComparable<Fraction>,
                             IEquatable<int>,
                             IEquatable<Integer>, IEquatable<Fraction>
    {
        private readonly uint[] numBits;
        private readonly int numLength;
        private readonly uint[] denomBits;
        private readonly int denomLength;
        private readonly bool sign;
    }
}

#endif
