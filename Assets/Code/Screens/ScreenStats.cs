using UnityEngine;
using System.Collections;
using System;

public sealed class ScreenStats : MenuScreen
{
    private TextGeneric _textName;

    public ScreenStats(GameObject goParent)
    {
        _go = new GameObject();
        _go.name = "statistics";

        _go.transform.SetParent(goParent.transform);

        setHeader("Player Statistics");
        
        setStatsTemplate();
        setBackToRankingsButton();

        toggleState(false);
    }



    private void setBackToRankingsButton()
    {
        //set header
        float width = Global._global.ScreenWidth * .225f;
        float height = width * .3f;
        int fontSize = Convert.ToInt32(width * .1f);

        Vector2 sizeDelta = new Vector2(width, height);

        float spacerHorizontal = width * .1f;
        Vector2 sizeDeltaHeader = new Vector2(width, height);
        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .15f);

        ButtonGeneric buttonGeneric = new ButtonGeneric(_go.transform, target, "btnBackToRankings", sizeDelta, fontSize, "Back to Rankings", "btnGenericLong");

        buttonGeneric._button.onClick.AddListener(delegate { Global._global._managerScreens.onButtonStatsBackToRatingsClicked(); });

    }

    private void setStatsTemplate()
    {
        //set header
        float width = Global._global.ScreenWidth * .25f;
        float height = width * .35f;
        int fontSize = Convert.ToInt32(width * .135f);

        float spacerHorizontal = width * .1f;
        Vector2 sizeDeltaHeader = new Vector2(width, height);
        Vector2 target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .5f);

        _textName = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Player name");
    }

    public void showStats(Player player)
    {
        _textName._uiText.text = player.Name;
    }
}
