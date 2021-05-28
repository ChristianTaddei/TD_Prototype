using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : UIElement
{
    private GameObject editButton;
    private GameObject playButton;

    public GameObject EditButton { get => editButton; }
    public GameObject PlayButton { get => playButton; }

    public TopBar() : base("TopBar")
    {
        float verticalPadding = 5, horizontalPadding = 5, buttonHeight = 30, buttonWidth = 100;

        editButton = InterfaceFactory.CreateButton(
            "Edit",
            canvas,
            new Vector2(horizontalPadding + buttonWidth / 2.0f, -verticalPadding - buttonHeight / 2.0f),
            new Vector2(buttonWidth, buttonHeight),
            Anchor.TOP_LEFT,
            /*InterfaceManager.EnterEditMode*/ null);

        playButton = InterfaceFactory.CreateButton(
            "Play",
            canvas,
            new Vector2(-horizontalPadding - buttonWidth / 2.0f, -verticalPadding - buttonHeight / 2.0f),
            new Vector2(buttonWidth, buttonHeight),
            Anchor.TOP_RIGHT,
            /*InterfaceManager.EnterPlayMode*/ null);
    }
}
