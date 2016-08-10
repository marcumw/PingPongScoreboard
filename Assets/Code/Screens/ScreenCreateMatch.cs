using UnityEngine;
using System.Collections;
using System;

public sealed class ScreenCreateMatch : MenuScreen {

    public ScreenCreateMatch(GameObject goParent)
    {
        _go = new GameObject();
        _go.name = "create match";

        _go.transform.SetParent(goParent.transform);

        setHeader("Create Match");

        toggleState(false);

        setSubmitButton();
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

        ButtonGeneric buttonGeneric = new ButtonGeneric(_go.transform, target, "btnSubmit", sizeDelta, fontSize, "Submit", "btnGenericLong");

        buttonGeneric._button.onClick.AddListener(delegate { Global._global._managerScreens.onButtonCreateMatchSubmitClicked(); });

    }
}
