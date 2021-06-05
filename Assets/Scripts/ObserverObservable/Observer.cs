using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    void update();

    IDisposable Register(Observable observable);
}
