using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private VisualElement root;
    private Button pathButton;
    private Button raiseButton;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        pathButton = root.Q<Button>("PathButton");

        pathButton.clickable = new Clickable(() =>
        {
            Debug.Log("path mode on");
            InterfaceManager.Instance.SetState(InterfaceManager.Instance.MakePathState);
        });

        raiseButton = root.Q<Button>("RaiseButton");
        raiseButton.clickable = new Clickable(() =>
        {
            Debug.Log("raise mode on");
            InterfaceManager.Instance.SetState(InterfaceManager.Instance.ModifyTerrainState);
        });
    }

    void Update()
    {

    }





}