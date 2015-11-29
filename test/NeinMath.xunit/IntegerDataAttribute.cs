using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace NeinMath.Xunit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IntegerDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var testType = testMethod.DeclaringType.AssemblyQualifiedName;
            var dataType = testType.Replace("Test, ", "Data, ");

            var type = Type.GetType(dataType);
            var data = Activator.CreateInstance(type);

            var exec = type.GetRuntimeMethod(testMethod.Name, new Type[0]);
            var test = (IEnumerable<object[]>)exec.Invoke(data, new object[0]);

            return test;
        }
    }
}
