using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class ScreenRanking : MenuScreen {

    private ButtonGeneric _btnCreateButton;

    public ScreenRanking(GameObject goParent)
    {
        _go = new GameObject();
        _go.name = "rankings";

        _go.transform.SetParent(goParent.transform);

        setHeader("Loading");

        setCreateMatchButton();

        toggleState(false);

        Global._global.onDelayedCall(.25f, onCheckIfDataFetchComplete);
    }

    private void onCheckIfDataFetchComplete()
    {
        if (Global._global._managerData._complete)
        {
            onDataFetchComplete();
        }
        else if (Global._global._managerData._error)
        {
            _textHeader._uiText.text = "Error";
        }
        else
        {
            Global._global.onDelayedCall(.25f, onCheckIfDataFetchComplete);
        }
    }

    private void onDataFetchComplete()
    {
        _textHeader._uiText.text = "Player Rankings";
        _btnCreateButton._go.SetActive(true);
        setPlayerButtons(Global._global._managerData._players);
    }

    private void setPlayerButtons(List<Player> players)
    {
        //set players
        float width = Global._global.ScreenWidth * .175f;
        float height = width * .35f;
        int fontSize = Convert.ToInt32(width * .115f);
        float spacerY = Global._global.ScreenHeight * .125f;

        Vector2 sizeDeltaNames = new Vector2(width, height);
        List<ButtonGeneric> buttons = new List<ButtonGeneric>();

        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .85f);

        players = players.OrderByDescending(p => p.Ratio).ToList();

        float currTargetY = Global._global.ScreenHeight * .75f;
        for (int i = 0; i < players.Count; i++)
        {
            Player currPlayer = players[i];
            target = new Vector2(Global._global.ScreenWidth / 2, currTargetY);

            ButtonGeneric buttonGeneric = new ButtonGeneric(_go.transform, target, "btn_" + currPlayer.Name, sizeDeltaNames, fontSize, currPlayer.Name + " #" + (i + 1).ToString(), "btnGenericLong");
            buttons.Add(buttonGeneric);

            buttonGeneric._button.onClick.AddListener(delegate { Global._global._managerScreens.onButtonPlayerClicked(currPlayer); });

            currTargetY -= spacerY;
        }
    }

    private void setCreateMatchButton()
    {
        //set header
        float width = Global._global.ScreenWidth * .225f;
        float height = width * .3f;
        int fontSize = Convert.ToInt32(width * .135f);

        Vector2 sizeDelta = new Vector2(width, height);

        float spacerHorizontal = width * .1f;
        Vector2 sizeDeltaHeader = new Vector2(width, height);
        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .125f);

        _btnCreateButton = new ButtonGeneric(_go.transform, target, "btnCreateMatch", sizeDelta, fontSize, "Create Match", "btnGenericLong");

        _btnCreateButton._button.onClick.AddListener(delegate { Global._global._managerScreens.onButtonRankingsCreateMatchClicked(); });

        _btnCreateButton._go.SetActive(false);
    }

    private void onButtonClicked(ButtonGeneric buttonGeneric)
    {

    }
}
