using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Builder<T>
{
	T Build();
}
