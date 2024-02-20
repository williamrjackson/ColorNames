using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ColorTester : MonoBehaviour
{
    public Image backgroundColorImage;
    public TextMeshProUGUI colorNameText;
    private ColorNames colorNames;
    private Color cachedColor;
    void Awake()
    {
        colorNames = new ColorNames();
    }

    void Update()
    {
        if (cachedColor != backgroundColorImage.color)
        {
            cachedColor = backgroundColorImage.color;
            colorNameText.SetText(colorNames.GetName(cachedColor));
            //https://en.wikipedia.org/wiki/YIQ
            float yiq = ((cachedColor.r * 299f) + (cachedColor.g * 596f) + (cachedColor.b * 211f)) / 1000f;
            colorNameText.color = (yiq >= .5f) ? Color.black : Color.white;
        }
    }
}
