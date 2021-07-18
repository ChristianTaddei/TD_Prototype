using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Moq;
using System.Reflection;

namespace Tests
{
	public class VectorTestsData
	{

		public static IEnumerable<Vector3> testVec3s
		{
			get => new List<Vector3>()
				{
					new Vector3(1,2,3),
					new Vector3(2,3,4)
				};
		}

		public static IEnumerable<(Vector3, Vector3)> equalVec3Pairs
		{
			get => new List<(Vector3, Vector3)>()
				{
					(new Vector3(1,2,3), new Vector3(1,2,3)),
					(new Vector3(2,3,4), new Vector3(2,3,4)),
				};
		}

		public static IEnumerable<(Vector3, Vector3)> differentVec3Pairs
		{
			get => new List<(Vector3, Vector3)>()
				{
					(new Vector3(1,2,3), new Vector3(3,4,5)),
					(new Vector3(2,3,4), new Vector3(5,5,5)),
				};
		}
	}
}