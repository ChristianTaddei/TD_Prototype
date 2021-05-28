using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper for a GameObject with a child canvas (and its rectTransform)
public class UIElement
{
    private GameObject interfacePrefab = (GameObject)Resources.Load("Prefabs/Interface/UI");

    protected GameObject mainGameObject;
    protected Transform canvas;
    protected RectTransform rectTransform;
    private float width;

    public float Width { get => width; set => width = value; }

    public UIElement(string name)
    {
        mainGameObject = (GameObject)GameObject.Instantiate(interfacePrefab);
        mainGameObject.name = name;

        canvas = mainGameObject.transform.Find("Canvas");

        rectTransform = canvas.GetComponent<RectTransform>();
        canvas.position = new Vector2(0,0);
        InterfaceFactory.setAnchor(rectTransform, Anchor.TOP_LEFT);
    }

    public void show(bool show){
        mainGameObject.GetComponentInChildren<Canvas>().enabled = show;
    }
}
