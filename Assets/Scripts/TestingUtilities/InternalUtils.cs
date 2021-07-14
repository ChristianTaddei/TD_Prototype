using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

// Started from https://codinghelmet.com/articles/testing-equals-and-gethashcode
internal class InternalUtils
{
	internal static TestResult SafeCall(string functionName, Func<TestResult> test)
	{

		try
		{
			return test();
		}
		catch (System.Exception ex)
		{

			string message =
				string.Format("{0} threw {1}: {2}",
							  functionName,
							  ex.GetType().Name,
							  ex.Message);

			return TestResult.CreateFailure(message);

		}

	}

	internal static MethodInfo GetOperator<T>(string methodName)
	{
		BindingFlags bindingFlags =
			BindingFlags.Static |
			BindingFlags.Public;
		MethodInfo equalityOperator =
			typeof(T).GetMethod(methodName, bindingFlags);
		return equalityOperator;
	}

	internal static void AssertAllTestsHavePassed(IList<TestResult> testResults)
	{

		bool allTestsPass =
			testResults
			.All(r => r.IsSuccess);
		string[] errors =
			testResults
			.Where(r => !r.IsSuccess)
			.Select(r => r.ErrorMessage)
			.ToArray();
		string compoundMessage =
			string.Join(Environment.NewLine, errors);

		Assert.IsTrue(allTestsPass,
					  "Some tests have failed:\n" +
					  compoundMessage);

	}
	
	internal static void ThrowIfAnyIsNull(params object[] objects)
	{
		if (objects.Any(o => object.ReferenceEquals(o, null)))
			throw new System.ArgumentNullException();
	}
}