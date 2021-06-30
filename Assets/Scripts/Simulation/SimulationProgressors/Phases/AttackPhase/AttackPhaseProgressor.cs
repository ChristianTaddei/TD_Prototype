using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackPhaseProgressor<T> : PhaseProgressor<T> where T : State
{
	
}
