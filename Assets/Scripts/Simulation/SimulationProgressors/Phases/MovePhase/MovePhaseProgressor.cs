using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovePhaseProgressor<T> : PhaseProgressor<T> where T : State
{
	
}
