using Xunit.Abstractions;
using Xunit.Sdk;

namespace NeinMath.Xunit
{
    public class IntegerDataDiscoverer : DataDiscoverer
    {
        public override bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            return false;
        }
    }
}
