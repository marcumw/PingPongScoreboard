using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public sealed class ScreenStats : MenuScreen
{
    private TextGeneric _textName;
    private TextGeneric _textAge;
    private TextGeneric _textMatchList;
    private TextGeneric _textWins;
    private TextGeneric _textLosses;

    private Player _currPlayer;

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
        Vector2 target = new Vector2(Global._global.ScreenWidth / 2, Global._global.ScreenHeight * .125f);

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

        Vector2 target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .75f);
        _textName = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Name: ", TextAnchor.MiddleLeft);

        target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .65f);
        _textAge = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Age: ", TextAnchor.MiddleLeft);

        target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .55f);
        _textMatchList = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Matches: ", TextAnchor.MiddleLeft);

        target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .45f);
        _textWins = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Wins: ", TextAnchor.MiddleLeft);

        target = new Vector2(Global._global.ScreenWidth * .5f, Global._global.ScreenHeight * .35f);
        _textLosses = new TextGeneric(_go.transform, target, "header", sizeDeltaHeader, fontSize, "Losses: ", TextAnchor.MiddleLeft);
    }

    public void onPlayerAgeFetched(bool error)
    {
        string agePostFix = _currPlayer.Age.ToString();

        if (error)
            agePostFix = "error";

        _textAge._uiText.text = "Age: " + agePostFix;
    }

    public void showStats(Player player)
    {
        Global._global._managerData.fetchPlayerAge(player);

        _currPlayer = player;

        _textName._uiText.text = "Name: " + _currPlayer.Name;
        _textAge._uiText.text = "Age:";

        if (player.Matches.Count > 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Match match in _currPlayer.Matches)
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(match.Id);
            }

            _textMatchList._uiText.text = "Matches: " + sb.ToString();
        }
        else
        {
            _textMatchList._uiText.text = "Matches: error";
        }

        _textWins._uiText.text = "Wins: " + _currPlayer.Wins;
        _textLosses._uiText.text = "Losses: " + _currPlayer.Losses;
    }
}
