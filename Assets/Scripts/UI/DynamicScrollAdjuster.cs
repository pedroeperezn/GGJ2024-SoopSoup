//Copyright (C) 2024 Soop Soup
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicScrollAdjuster : MonoBehaviour
{
    private void Start()
    {
        CalculateHeightOfArea();
    }
    public void CalculateHeightOfArea()
    {
        // get the image components of all children as well as spacing between each
        Image[] allChildren = GetComponentsInChildren<Image>();
        float spacing = GetComponent<VerticalLayoutGroup>().spacing;

        // create height variable and set to 0
        float height = 0;

        // iterate through each child and add their height and the spacing
        foreach (Image child in allChildren)
        {
            height += child.GetComponent<RectTransform>().rect.height + spacing;
        }

        // once height is calculated, set this object height to what it needs
        RectTransform thisObject = GetComponent<RectTransform>();
        thisObject.sizeDelta = new Vector2(thisObject.rect.width, height);
    }
}
