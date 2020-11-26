﻿using UnityEngine;
using UnityEngine.UI;

public class TextScaler : MonoBehaviour
{
    private const int ScaleValue = 10;

    private Text _text;
    void Start()
    {
        _text = gameObject.GetComponent<Text>();

        _text.fontSize = _text.fontSize * ScaleValue;

        _text.transform.localScale = _text.transform.localScale / ScaleValue;

        _text.horizontalOverflow = HorizontalWrapMode.Overflow;
        _text.verticalOverflow = VerticalWrapMode.Overflow;
    }
}
