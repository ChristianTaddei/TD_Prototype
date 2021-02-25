using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : Representable<TowerRepresentation>
{
    public static float Range { get => 6.0f; }

    public Tower()
    {

    }

    public override string PrefabString => "tower";
}
