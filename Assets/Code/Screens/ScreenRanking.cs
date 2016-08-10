using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public sealed class ScreenRanking : MenuScreen {

    public ScreenRanking(GameObject goParent)
    {
        _go = new GameObject();
        _go.name = "rankings";

        _go.transform.SetParent(goParent.transform);

        setHeader("Player Ranking");

        setCreateMatchButton();

        toggleState(false);

        //Global._global.onDelayedCall(.25f, onCheckIfPlayerDataFetched);
    }

    private void setPlayerButtons(List<Player> players)
    {
        //set players
        float width = Global._global.ScreenWidth * .175f;
        float height = width * .35f;
        int fontSize = Convert.ToInt32(width * .135f);
        float spacerY = Global._global.ScreenHeight * .15f;

        Vector2 sizeDeltaNames = new Vector2(width, height);
        List<ButtonGeneric> buttons = new List<ButtonGeneric>();

        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .85f);

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
        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .15f);

        ButtonGeneric buttonGeneric = new ButtonGeneric(_go.transform, target, "btnCreateMatch", sizeDelta, fontSize, "Create Match", "btnGenericLong");

        buttonGeneric._button.onClick.AddListener(delegate { Global._global._managerScreens.onButtonRankingsCreateMatchClicked(); });

    }

    private void onButtonClicked(ButtonGeneric buttonGeneric)
    {

    }
}
