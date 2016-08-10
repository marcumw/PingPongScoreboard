using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public sealed class ButtonGeneric
{
    public GameObject _go;
    public RectTransform _rt;
    public Button _button;
    public CanvasGroup _cg;

    public GameObject _goText;
    public Text _uiText;

    public string _textValue;
    private static Font _font;
    private int _fontSize = 25;
    public Vector2 _target;

    public Image _image;
    private Sprite _sprite;
    private Sprite _spriteSelected;
    public static Texture2D _texture;
    public static Texture2D _textureSelected;

    public ButtonGeneric(Transform parentTransform, Vector2 target, string name, Vector2 sizeDelta, int fontSize, 
                                                string textValue = "", string defaultImage = "btnYear")
    {
        //_texture = (Texture2D)Resources.Load(defaultImage);
        //Sprite uiSprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));

        _target = target;
        _textValue = textValue;
        _fontSize = fontSize;

        if (_font == null)
        {
            _font = _font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

            //_colorInactive = Utils.HexToColor("747474");
        }

        _go = new GameObject();
        _go.name = name;
        _go.transform.SetParent(parentTransform, false);    
        _go.transform.localScale = Vector3.one;

        _cg = _go.AddComponent<CanvasGroup>();

        _button = _go.AddComponent<Button>();

        if (_texture == null)
        {
            _texture = (Texture2D)Resources.Load("btnGeneric");
            _textureSelected = (Texture2D)Resources.Load("btnGenericSelected");
        }

        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        _spriteSelected = Sprite.Create(_textureSelected, new Rect(0, 0, _textureSelected.width, _textureSelected.height), new Vector2(0.5f, 0.5f));

        _image = _go.AddComponent<Image>();
        _image.sprite = _sprite;
        _button.image = _image;

        _rt = _button.GetComponent<RectTransform>();

        if (sizeDelta.y == 0)
        {
            float ratio = (float)_texture.height / (float)_texture.width;
            _rt.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.x * ratio);
        }
        else
        {
            _rt.sizeDelta = sizeDelta;
        }

        _rt.anchoredPosition = target;

        setText();
    }

    private void setText()
    {
        //_fontSize = (int)(_rt.sizeDelta.y * .35f);

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
    }

    public static void toggleSelected(ButtonGeneric button, bool toggle)
    {
        if (toggle)
        {
            button._uiText.color = Color.black;
            //button._uiText.fontStyle = FontStyle.Bold;
            button._image.sprite = button._spriteSelected;
        }
        else
        {
            button._uiText.color = Color.white;
            //button._uiText.fontStyle = FontStyle.Normal;
            button._image.sprite = button._sprite;
        }
    }
}

