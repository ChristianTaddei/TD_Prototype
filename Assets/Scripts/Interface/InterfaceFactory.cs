using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// TODO: other name? its not exactly factory
public static class InterfaceFactory
{
    private static GameObject interfacePrefab = (GameObject)Resources.Load("Prefabs/Interface/UI");
    private static GameObject buttonPrefab = (GameObject)Resources.Load("Prefabs/Interface/Button");
    private static GameObject sliderPrefab = (GameObject)Resources.Load("Prefabs/Interface/Slider");
    private static GameObject labelPrefab = (GameObject)Resources.Load("Prefabs/Interface/Label");
    private static GameObject popupPrefab = (GameObject)Resources.Load("Prefabs/Interface/SaveAsPopup");

    public static GameObject CreateMainGameObject(string name, Vector2 position, Anchor anchor)
    {
        GameObject uiGameObject = (GameObject)GameObject.Instantiate(interfacePrefab);
        uiGameObject.name = name;

        Transform canvas = uiGameObject.transform.Find("Canvas");

        RectTransform rt = canvas.GetComponent<RectTransform>();
        rt.position = position;
        setAnchor(rt, anchor);

        return uiGameObject;
    }

    public static GameObject createButton(string name, Transform canvas, Vector2 position, Vector2 size, Anchor anchor, UnityAction callback)
    {
        GameObject button = (GameObject)GameObject.Instantiate(
            buttonPrefab,
            canvas.transform);

        button.name = name;
        button.GetComponentInChildren<Text>().text = name;

        RectTransform rt = button.GetComponent<RectTransform>();
        rt.position = position;
        rt.sizeDelta = size;
        setAnchor(rt, anchor);

        button.GetComponent<Button>().onClick.AddListener(callback);

        return button;
    }

    public static GameObject createSlider(string name, Transform canvas, Vector2 position,
        Vector2 size, Anchor anchor, float min, float max, UnityAction<float> callback)
    {
        GameObject slider = (GameObject)GameObject.Instantiate(
            sliderPrefab,
            canvas.transform);

        slider.name = name;
        slider.GetComponentInChildren<Text>().text = name;

        RectTransform rt = slider.GetComponent<RectTransform>();
        rt.position = position;
        rt.sizeDelta = size;
        setAnchor(rt, anchor);

        Slider sliderComponent = slider.GetComponent<Slider>();
        sliderComponent.minValue = min;
        sliderComponent.maxValue = max;
        sliderComponent.onValueChanged.AddListener(callback);

        return slider;
    }

    internal static GameObject createLabel(string name, Transform canvas, Vector2 position, Vector2 size, Anchor anchor, Func<string> labelUpdateCallback)
    {
       GameObject label = (GameObject)GameObject.Instantiate(
            labelPrefab,
            canvas.transform);

        label.name = name;
        label.GetComponentInChildren<Text>().text = name;

        label.GetComponentInChildren<LabelUpdater>().updateFunction = labelUpdateCallback;

        RectTransform rt = label.GetComponent<RectTransform>();
        rt.position = position;
        rt.sizeDelta = size;
        setAnchor(rt, anchor);

        return label;
    }

    public static GameObject createPopup(string name, Transform canvas, Vector2 position,
        Anchor anchor, UnityAction<string> callback)
    {
        GameObject popup = (GameObject)GameObject.Instantiate(
            popupPrefab,
            canvas.transform);

        popup.name = name;
        //popup.GetComponentInChildren<Text>().text = name;

        RectTransform rt = popup.GetComponent<RectTransform>();
        rt.position = position;
        // rt.sizeDelta = size;
        setAnchor(rt, anchor);

        Button confirmButton = popup.GetComponentInChildren<Button>();
        InputField inputField = popup.GetComponentInChildren<InputField>();
        UnityAction confirmCallback = () =>
        {
            callback(inputField.text);
            GameObject.Destroy(popup); // FIXME: does not remove from editor, nor does DestroyImmediate
        };
        confirmButton.onClick.AddListener(confirmCallback);

        return popup;
    }

    public static void setAnchor(RectTransform rectTransform, Anchor anchor)
    {
        switch (anchor)
        {
            case Anchor.TOP_LEFT:
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.anchorMin = new Vector2(0, 1);
                break;
            case Anchor.TOP:
                rectTransform.anchorMax = new Vector2(0.5f, 1);
                rectTransform.anchorMin = new Vector2(0.5f, 1);
                break;
            case Anchor.TOP_RIGHT:
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.anchorMin = new Vector2(1, 1);
                break;
            case Anchor.CENTRE:
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                break;
            default:
                break;
        }
    }
}