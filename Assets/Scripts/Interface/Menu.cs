using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private VisualElement root;
    private Button pathButton;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        pathButton = root.Q<Button>("PathButton");

        pathButton.clickable = new Clickable(() =>
        {
            Debug.Log("path mode on");
            // InterfaceManager.OnClick = InterfaceManager.MakePath;
        });
    }

    void Update()
    {

    }





}
