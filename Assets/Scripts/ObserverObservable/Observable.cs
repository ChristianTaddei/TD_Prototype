using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observable
{
    IList<Observer> Observers {get;}

    void Notify();

    IDisposable Subscribe(Observer observer);
}
