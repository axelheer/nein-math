using System;

#if SOMETIME

namespace NeinMath
{
    /// <summary>
    /// Represents an arbitrarily large signed rational.
    /// </summary>
    public struct Rational : IComparable,
                             IComparable<int>,
                             IComparable<Integer>, IComparable<Rational>,
                             IEquatable<int>,
                             IEquatable<Integer>, IEquatable<Rational>
    {
        private readonly uint[] numBits;
        private readonly int numLength;
        private readonly uint[] denomBits;
        private readonly int denomLength;
        private readonly bool sign;
    }
}

#endif
