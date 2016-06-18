using System;
using Xunit;

namespace NeinMath.Tests
{
    public static class Immutability
    {
        public static IDisposable Guard<T>(T v0)
        {
            return new ImmutabilityGuard<T>(v0);
        }

        public static IDisposable Guard<T>(T v0, T v1)
        {
            return new ImmutabilityGuard<T>(v0, v1);
        }

        public static IDisposable Guard<T>(T v0, T v1, T v2)
        {
            return new ImmutabilityGuard<T>(v0, v1, v2);
        }

        sealed class ImmutabilityGuard<T> : IDisposable
        {
            readonly T v0;
            readonly T v1;
            readonly T v2;

            readonly string t0;
            readonly string t1;
            readonly string t2;

            public ImmutabilityGuard(T v0)
                : this(v0, default(T))
            {
            }

            public ImmutabilityGuard(T v0, T v1)
                : this(v0, v1, default(T))
            {
            }

            public ImmutabilityGuard(T v0, T v1, T v2)
            {
                t0 = v0?.ToString();
                t1 = v1?.ToString();
                t2 = v2?.ToString();

                this.v0 = v0;
                this.v1 = v1;
                this.v2 = v2;
            }

            public void Dispose()
            {
                Assert.Equal(t0, v0?.ToString());
                Assert.Equal(t1, v1?.ToString());
                Assert.Equal(t2, v2?.ToString());
            }
        }
    }
}
