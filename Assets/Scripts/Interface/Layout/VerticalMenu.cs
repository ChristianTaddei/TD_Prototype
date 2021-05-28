using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public abstract class VerticalMenu : UIElement
{
    protected float horizontalPadding, verticalPadding, buttonWidth, buttonHeight;
    protected float currentVerticalPosition = 0;
    protected Anchor buttonsAnchor;

    public VerticalMenu(string name, Anchor buttonsAnchor, float horizontalPadding, float verticalPadding,
        float buttonWidth, float buttonHeight) : base(name)
    {
        this.buttonsAnchor = buttonsAnchor;
        this.horizontalPadding = horizontalPadding;
        this.verticalPadding = verticalPadding;
        this.buttonWidth = buttonWidth;
        this.buttonHeight = buttonHeight;

        OnMenuOpen();
    }

    protected virtual void OnMenuOpen()
    {
        
    }

    protected virtual void  OnMenuClose()
    {
        
    }

    protected GameObject AddButton(string name, UnityAction callback)
    {
        float direction = 1;
        if (buttonsAnchor == Anchor.TOP_RIGHT) direction = -1;

        GameObject button = InterfaceFactory.CreateButton(
            name,
            this.canvas,
            new Vector2(direction * (horizontalPadding + buttonWidth / 2.0f), currentVerticalPosition),
            new Vector2(buttonWidth, buttonHeight),
            buttonsAnchor,
            callback);

        currentVerticalPosition -= verticalPadding + buttonHeight;

        return button;
    }

    protected GameObject AddSlider(string name, float min, float max, UnityAction<float> callback)
    {
        float direction = 1;
        if (buttonsAnchor == Anchor.TOP_RIGHT) direction = -1;

        GameObject button = InterfaceFactory.CreateSlider(
            name,
            canvas,
            new Vector2(direction * (horizontalPadding + buttonWidth / 2.0f), currentVerticalPosition),
            new Vector2(buttonWidth, buttonHeight * 0.6f),
            buttonsAnchor,
            min, max,
            callback);

        currentVerticalPosition -= verticalPadding + buttonHeight;

        return button;
    }

    protected GameObject AddLabel(string name, Func<String> labelUpdateCallback)
    {
        float direction = 1;
        if (buttonsAnchor == Anchor.TOP_RIGHT) direction = -1;

        GameObject button = InterfaceFactory.CreateLabel(
            name,
            canvas,
            new Vector2(direction * (horizontalPadding + buttonWidth / 2.0f), currentVerticalPosition),
            new Vector2(buttonWidth, buttonHeight * 0.6f),
            buttonsAnchor,
            labelUpdateCallback);

        currentVerticalPosition -= verticalPadding + buttonHeight;
        
        return button;
    }

    protected void MoveVerticalPosition(float distance)
    {
        currentVerticalPosition += distance;
    }
}
