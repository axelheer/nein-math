using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Xunit.v3;

#pragma warning disable CA1062
#pragma warning disable CA1307

namespace NeinMath.Tests
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class IntegerDataAttribute : DataAttribute
    {
        public override bool SupportsDiscoveryEnumeration() => true;

        public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
        {
            var testType = testMethod.DeclaringType.AssemblyQualifiedName;
            var dataType = testType.Replace("Test, ", "Data, ");

            var type = Type.GetType(dataType);
            var data = Activator.CreateInstance(type);

            var exec = type.GetRuntimeMethod(testMethod.Name, new Type[0]);
            var test = (IReadOnlyCollection<ITheoryDataRow>)exec.Invoke(data, new object[0]);

            return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(test);
        }
    }
}
