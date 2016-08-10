using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;
using System.Text.RegularExpressions;

public class ManagerData  {

    public int _countMatchesFetched = 0;
    public List<Player> _players = new List<Player>();


    public ManagerData()
    {

    }

    public void load()
    {
        fetchPlayersData();
    }

    #region players data
    private void fetchPlayersData()
    {
        List<Player> players = new List<Player>();

        string urlPlayers = "https://demo1743076.mockable.io/player";
  
        WebCall webCall = new WebCall(Global._global, urlPlayers);

        webCall.OnDone += w =>
        {
            if (w.Error == null)
            {
                unpackPlayersJson(w.Text);
            }
            else
            {
                _players = null;
            }

            w.Dispose();
        };

    }

    private void unpackPlayersJson(string json)
    {
        string jsonRaw = "{\"players\":" + Regex.Replace(json, @"\s+", "") + "}";
        List<PlayerJson> playersJson = JsonUtility.FromJson<Players>(jsonRaw).players;

        foreach (PlayerJson player in playersJson)
        {
            _players.Add(new Player(player.id, player.name));
        }

        onPlayersDataFetchComplete();
    }

    private void onPlayersDataFetchComplete()
    {
        fetchMatchesData();
    }
    #endregion

    #region matches data
    private void fetchMatchesData()
    {
        foreach (Player player in _players)
        {
            fetchMatchesDataByPlayerId(player);
        }
    }

    public void fetchMatchesDataByPlayerId(Player player)
    {
        string urlPlayers = "https://demo1743076.mockable.io/match?player=" + player.Id;

        WebCall webCall = new WebCall(Global._global, urlPlayers);

        webCall.OnDone += w =>
        {
            _countMatchesFetched++;

            if (w.Error == null)
            {
                unpackMatchesJson(w.Text, player);
            }
            else
            {
                
            }

            if (_countMatchesFetched == _players.Count)
            {
                onMatchesDataFetchComplete();
            }

            w.Dispose();
        };
    }

    private void unpackMatchesJson(string json, Player player)
    {
        string jsonRaw = "{\"matches\":" + Regex.Replace(json, @"\s+", "") + "}";

        JSONArray matchNodes = JSONNode.Parse(jsonRaw)["matches"].AsArray;
        JSONArray playerNodes;

        int countMatches = matchNodes.Count;

        for (int i = 0; i < matchNodes.Count; i++)
        {
            playerNodes = matchNodes[i]["players"].AsArray;

            List<PlayerScore> playerScores = new List<PlayerScore>();
            for (int j = 0; j < playerNodes.Count; j++)
            {
                playerScores.Add(new PlayerScore(playerNodes[j]["id"].AsInt, playerNodes[j]["score"].AsInt));
            }

            player._matches.Add(new Match(matchNodes[i]["id"].AsInt, matchNodes[i]["timeStart"].Value, playerScores));
        }

        onMatchesDataFetchComplete();
    }

    private void onMatchesDataFetchComplete()
    {

    }

    #endregion
}


#region serializable json objects

#region player serialization
[Serializable]
public class Players
{
    public List<PlayerJson> players;
}

[Serializable]
public class PlayerJson
{
    public int id;
    public string name;
}

#endregion

#endregion
