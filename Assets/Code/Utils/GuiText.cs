using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public sealed class GuiText {

    public GameObject _go;
    public Text _guiText;
    public CanvasGroup _cg;

    private string _text;
    private float _width;
    private float _height;

    public float Width
    {
        get { return _guiText.preferredWidth; }
        set { _width = value; UpdateDelta(); }
    }
    public float Height
    {
        get { return _guiText.preferredHeight; }
        set { _height = value; }
    }

    private Vector2 _localPosition;

    private Vector2 _sizeDelta;

    public string text
    {
        get { return _text; }
        set { 
            _text = value; 
            _guiText.text = _text;
            UpdateDelta();
        }
    }

    public GuiText(Transform parentTransform, string name, string text, int fontSize, string color = "ffffff", FontStyle style = FontStyle.Normal)
    {
        //set up progress text
        _go = new GameObject();
        _go.name = name;

        _cg = _go.AddComponent<CanvasGroup>();
        _guiText = _go.AddComponent<Text>();
        _guiText.transform.SetParent(parentTransform, false);

        _guiText.text = text;
        _guiText.fontStyle = style;
        _guiText.fontSize = fontSize;
        _guiText.color = Global.HexToColor(color);
        _guiText.alignment = TextAnchor.MiddleCenter;
        _guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _guiText.verticalOverflow = VerticalWrapMode.Overflow;
        _guiText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        _guiText.rectTransform.sizeDelta = new Vector2(_guiText.preferredWidth, fontSize);
    }

    public void SetPosition(float x, float y)
    {
        _guiText.rectTransform.anchoredPosition = new Vector3(x, y, 0);

    }

    private void UpdateDelta()
    {
        _sizeDelta.x = _guiText.preferredWidth;
        _sizeDelta.y = _guiText.preferredHeight;

        _guiText.rectTransform.sizeDelta = _sizeDelta;
    }

    public void SetActive(bool value)
    {
        _go.SetActive(value);
    }

    public void SetAlpha(float alpha)
    {
        _cg.alpha = alpha;
    }

}
