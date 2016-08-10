using UnityEngine;
using System.Collections;

public sealed class ManagerScreens {

    private GameObject _go;
    private Canvas _canvas;
    private ScreenCreateMatch _screenCreateMatch;
    private ScreenRanking _screenRanking;
    private ScreenStats _screenStats;

    public ManagerScreens()
    {

    }

    #region init

    public void load(Canvas canvas)
    {
        _canvas = canvas;

        init();
        initScreens();

        toggleScreen(_screenRanking, true);
    }

    private void init()
    {
        _go = new GameObject();
        _go.name = "Screens";

        _go.transform.SetParent(_canvas.transform);

        RectTransform rt = _go.AddComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;// new Vector2(Global._global.ScreenHeight / 2, Global._global.ScreenWidth / 2);
    }

    private void initScreens()
    {
        _screenCreateMatch = new ScreenCreateMatch(_go);
        _screenRanking = new ScreenRanking(_go);
        _screenStats = new ScreenStats(_go);
    }
    #endregion

    #region toggle logic
    private void toggleScreen(MenuScreen menu, bool toggle)
    {
        menu.toggleState(toggle);
    }

    private void toggleOff()
    {
        toggleScreen(_screenRanking, false);
        toggleScreen(_screenStats, false);
        toggleScreen(_screenCreateMatch, false);
    }
    #endregion

    #region touch logic
    public void onButtonPlayerClicked(Player player)
    {
        toggleScreen(_screenRanking, false);
        toggleScreen(_screenStats, true);

        _screenStats.showStats(player);
    }

    public void onButtonRankingsCreateMatchClicked()
    {
        toggleScreen(_screenRanking, false);
        toggleScreen(_screenCreateMatch, true);
    }

    public void onButtonCreateMatchSubmitClicked()
    {
        toggleScreen(_screenRanking, true);
        toggleScreen(_screenCreateMatch, false);
    }

    public void onButtonStatsBackToRatingsClicked()
    {
        toggleScreen(_screenRanking, true);
        toggleScreen(_screenStats, false);
    }
    #endregion

}

public enum ScreenType
{
    CreateMatch,
    Rankings,
    Stats
}


