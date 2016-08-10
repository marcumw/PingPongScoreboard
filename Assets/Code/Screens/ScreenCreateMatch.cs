using UnityEngine;
using System.Collections;
using System;

public sealed class ScreenCreateMatch : MenuScreen {

    private GuiInputField _ifDate;
    private GuiInputField _ifName;
    private GuiInputField _ifScore;
    private ButtonGeneric _btnSubmit;

    private CanvasGroup _cv;

    public ScreenCreateMatch(GameObject goParent)
    {
        _go = new GameObject();
        _go.name = "create match";

        _go.transform.SetParent(goParent.transform);

        _cv = _go.AddComponent<CanvasGroup>();

        setHeader("Create Match");

        toggleState(false);

        setSubmitButton();
        setUpInputFields();
    }

    private void setSubmitButton()
    {
        //set header
        float width = Global._global.ScreenWidth * .225f;
        float height = width * .3f;
        int fontSize = Convert.ToInt32(width * .135f);

        Vector2 sizeDelta = new Vector2(width, height);

        float spacerHorizontal = width * .1f;
        Vector2 sizeDeltaHeader = new Vector2(width, height);
        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .15f);

        _btnSubmit = new ButtonGeneric(_go.transform, target, "btnSubmit", sizeDelta, fontSize, "Submit", "btnGenericLong");

        _btnSubmit._button.onClick.AddListener(delegate { onButtonSubmitClicked(); });

        _btnSubmit._button.interactable = false;
    }

    private void onButtonSubmitClicked()
    {
        _cv.alpha = .65f;

        string validatedDate = _ifDate._inputField.text;
        string validatedName = _ifName._inputField.text;
        string validatedScore = _ifScore._inputField.text;

        //add validation here

        Global._global._managerData.saveMatchData(validatedDate, validatedName, validatedScore);
        Global._global.onDelayedCall(1, onDelayForSaveComplete);
    }

    private void onDelayForSaveComplete()
    {
        _cv.alpha = 1;

        _ifDate._inputField.interactable = true;
        _ifName._inputField.interactable = true;
        _ifScore._inputField.interactable = true;

        GuiInputField.reset(_ifDate);
        GuiInputField.reset(_ifName);
        GuiInputField.reset(_ifScore);

        Global._global._managerScreens.onButtonCreateMatchSubmitClicked();
    }

    private void setUpInputFields()
    {
        float width = Global._global.ScreenWidth * .3f;
        float height = width * .2f;
        float spacerY = Global._global.ScreenHeight * .125f;

        Vector2 sizeDelta = new Vector2(width, height);
        Vector2 target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .65f);

        _ifDate = new GuiInputField("ifDate", "enter date", target, _go.transform, sizeDelta, TouchScreenKeyboardType.URL);
        _ifDate._inputField.onValueChanged.AddListener(delegate { onInputFieldValueChanged(); });

        target.y -= spacerY;
        _ifName = new GuiInputField("ifName", "enter name", target, _go.transform, sizeDelta, TouchScreenKeyboardType.URL);
        _ifName._inputField.onValueChanged.AddListener(delegate { onInputFieldValueChanged(); });

        target.y -= spacerY;
        _ifScore = new GuiInputField("ifScore", "enter score", target, _go.transform, sizeDelta, TouchScreenKeyboardType.URL);
        _ifScore._inputField.onValueChanged.AddListener(delegate { onInputFieldValueChanged(); });

    }

    private void onInputFieldValueChanged()
    {
        if (_ifDate._inputField.text.Trim().Length != 0
            && _ifName._inputField.text.Trim().Length != 0
            && _ifScore._inputField.text.Trim().Length != 0)
        {
            _btnSubmit._button.interactable = true;
        }
        else
        {
            _btnSubmit._button.interactable = false;
        }
    }
}
