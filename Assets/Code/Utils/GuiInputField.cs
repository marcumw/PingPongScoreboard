using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public sealed class GuiInputField
{
    public GameObject _go;
    public GuiText _gt;
    public RectTransform _rt;
    public GuiText _gtPlaceholder;
    public InputField _inputField;
    private Sprite _sprite;
    private Image _image;

    public string _initValue;
    private Vector3 _target;
    private Vector2 _sizeDelta;

   public string _name;
    private int _fontSizeDefault;
    private int _fontSizePlaceholder;
    private bool _reset = false;

    public static Texture2D _texture;

    public GuiInputField(string name, string initValue, Vector3 target, Transform parent, Vector2 sizeDelta, 
                                TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default,
                                InputField.InputType inputType = InputField.InputType.Standard)
    {
        _go = new GameObject();
        _go.name = name;
        _go.transform.parent = parent;

        _rt = _go.AddComponent<RectTransform>();

        _name = name;
        _target = target;
        _initValue = initValue;
        _sizeDelta = sizeDelta;

        _rt.anchoredPosition = _target;

        _fontSizeDefault = (int)(_sizeDelta.y * .4f);
        _fontSizePlaceholder = (int)(_sizeDelta.y * .4f);

        _gt = new GuiText(_go.transform, name + "TextBox", "", _fontSizeDefault);
        _gt._guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gt._guiText.rectTransform.sizeDelta = new Vector2(_sizeDelta.x, _gt._guiText.preferredHeight);
        _gt._guiText.transform.localPosition = Vector3.zero;

        if (_texture == null)
        {
            _texture = (Texture2D)Resources.Load("longBackgroundDark");
        }

        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        _image = _go.AddComponent<Image>();

        //float ratio = (float)_texture.height / (float)_texture.width;
        _image.rectTransform.sizeDelta = _sizeDelta;
        _image.sprite = _sprite;
        //_image.color = Color.black;

        _inputField = _go.AddComponent<InputField>();
        _inputField.lineType = InputField.LineType.SingleLine;
        _inputField.textComponent = _gt._guiText;
        _inputField.image = _image;
        _inputField.keyboardType = keyboardType;
        _inputField.characterLimit = 50;
        _inputField.shouldHideMobileInput = true;
        _inputField.inputType = inputType;

        //_inputField.onValueChange.AddListener(delegate { onInputFieldValueChange(); });

        _gtPlaceholder = new GuiText(_go.transform, name + "Placeholder", _initValue, _fontSizePlaceholder, "545454", FontStyle.Italic);
        _gtPlaceholder._guiText.horizontalOverflow = HorizontalWrapMode.Overflow;

        _inputField.placeholder = _gtPlaceholder._guiText;

        //OnPointerDownOverride onPointerDown = _inputField.gameObject.AddComponent<OnPointerDownOverride>();
        //onPointerDown._tfo = this;

        //OnPointerExitOverride onPointerExit = _inputField.gameObject.AddComponent<OnPointerExitOverride>();
        //onPointerExit._tfo = this;

    }

    public static void reset(GuiInputField to)
    {
        to._reset = true;
        to._inputField.text = "";
        to._inputField.characterLimit = 50;
        to._gt._guiText.fontSize = to._fontSizeDefault;
        to._gtPlaceholder._guiText.text = to._initValue;
        to._gtPlaceholder._guiText.color = Global.HexToColor("545454");
    }

}