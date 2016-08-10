using UnityEngine;
using System.Collections;
using System;

public class MenuScreen  {

    public TextGeneric _textHeader;
    public GameObject _go;

    public MenuScreen()
    {

    }

    public void toggleState(bool toggle)
    {
        _go.SetActive(toggle);
    }

    public void setHeader(string title)
    {
        //set header
        float width = Global._global.ScreenWidth * .3f;
        float height = width * .35f;
        int fontSize = Convert.ToInt32(width * .15f);

        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .9f);

        float spacerHorizontal = width * .1f;
        Vector2 sizeDeltaHeader = new Vector2(width, height);

        _textHeader = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, title);
    }
}
