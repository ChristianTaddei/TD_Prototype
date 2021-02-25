using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelUpdater : MonoBehaviour
{
    public Text textToUpdate;
    public Func<String> updateFunction {get; set;}

    void Start()
    {
        
    }

    void Update()
    {
        textToUpdate.text = updateFunction();
    }
}
