using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public sealed class TextGeneric
{
    public GameObject _go;
    public RectTransform _rt;
    public CanvasGroup _cg;

    public GameObject _goText;
    public Text _uiText;

    public string _textValue;
    private static Font _font;
    private int _fontSize = 25;
    public Vector2 _target;

    public TextGeneric(Transform parentTransform, Vector2 target, string name, Vector2 sizeDelta, int fontSize, 
                                                string textValue = "")
    {
        //_texture = (Texture2D)Resources.Load(defaultImage);
        //Sprite uiSprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));

        _target = target;
        _textValue = textValue;
        _fontSize = fontSize;

        if (_font == null)
        {
            _font = _font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        }

        _go = new GameObject();
        _go.name = name;
        _go.transform.SetParent(parentTransform, false);    
        _go.transform.localScale = Vector3.one;

        _cg = _go.AddComponent<CanvasGroup>();

        _goText = new GameObject();
        _goText.name = "uiText";
        _goText.transform.parent = _go.transform;

        _uiText = _goText.AddComponent<Text>();
        _uiText.text = _textValue;
        _uiText.font = _font;
        _uiText.fontSize = _fontSize;
        _uiText.fontStyle = FontStyle.Normal;

        _uiText.alignment = TextAnchor.MiddleCenter;
        _uiText.rectTransform.anchoredPosition = Vector2.zero;
        _uiText.verticalOverflow = VerticalWrapMode.Overflow;
        _uiText.horizontalOverflow = HorizontalWrapMode.Overflow;

        _rt = _goText.GetComponent<RectTransform>();
        _rt.sizeDelta = sizeDelta;

        _rt.anchoredPosition = target;

        setText();
    }

    private void setText()
    {

    }
}

