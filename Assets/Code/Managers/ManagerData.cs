using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;
using System.Text.RegularExpressions;

public class ManagerData  {

    public int _countPlayerMatchesFetched = 0;

    private Player _playerLast;
    public List<Player> _players = new List<Player>();

    public bool _complete = false;
    public bool _error = false;

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
                _error = true;
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
            Player newPlayer = new Player(player.id, player.name);
            _players.Add(newPlayer);
        }

        onPlayersDataFetchComplete();
    }

    private void onPlayersDataFetchComplete()
    {
        fetchMatchesData();
    }

    public void fetchPlayerAge(Player player)
    {
        _playerLast = player;
        string urlPlayer = "https://demo1743076.mockable.io/player/" + player.Id;

        WebCall webCall = new WebCall(Global._global, urlPlayer);

        webCall.OnDone += w =>
        {
            unpackPlayerAgeJson(w.Text);
            w.Dispose();
        };
    }

    private void unpackPlayerAgeJson(string jsonRaw)
    {
        string json = Regex.Replace(jsonRaw, @"\s+", "");
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(jsonRaw);

        if (json.Contains("error"))
        {
            _playerLast.Age = -1;
        }
        else
        {
            _playerLast.Age = playerJson.age;
        }
    }
    #endregion

    #region matches data 
    private void fetchMatchesData()
    {
        foreach (Player player in _players)
        {
            fetchMatchesDataByPlayerId(player.Id);
        }
    }

    public void fetchMatchesDataByPlayerId(int playerId)
    {
        string urlPlayers = "https://demo1743076.mockable.io/match?player=" + playerId;

        WebCall webCall = new WebCall(Global._global, urlPlayers);

        webCall.OnDone += w =>
        {
            _countPlayerMatchesFetched++;

            if (w.Error == null)
            {
                unpackMatchesJson(w.Text, playerId);
            }
            else
            {
                
            }

            if (_countPlayerMatchesFetched == _players.Count)
            {
                onMatchesDataFetchComplete();
            }

            w.Dispose();
        };
    }

    private void unpackMatchesJson(string json, int playerId)
    {
        string jsonRaw = "{\"matches\":" + Regex.Replace(json, @"\s+", "") + "}";

        JSONArray playerNodes;
        JSONArray matchNodes = JSONNode.Parse(jsonRaw)["matches"].AsArray;

        List<Match> matches = new List<Match>();

        int countMatches = matchNodes.Count;

        for (int i = 0; i < matchNodes.Count; i++)
        {
            playerNodes = matchNodes[i]["players"].AsArray;

            List<MatchScore> matchScores = new List<MatchScore>();
            for (int j = 0; j < playerNodes.Count; j++)
            {
                matchScores.Add(new MatchScore(playerNodes[j]["id"].AsInt, playerNodes[j]["score"].AsInt));
            }

            Match newMatch = new Match(matchNodes[i]["id"].AsInt, matchNodes[i]["timeStart"].Value, matchScores);

            matches.Add(newMatch);
        }

        addMatchesByPlayerId(playerId, matches);
    }

    private void addMatchesByPlayerId(int playerId, List<Match> matches)
    {
        foreach (Player player in _players)
        {
            if (player.Id == playerId)
            {
                player.Matches = matches;
                break;
            }
        }
    }

    private void onMatchesDataFetchComplete()
    {
        foreach (Player player in _players)
        {
            Player.setWinsAndLosses(player);
        }

        _complete = true;
    }

    public void saveMatchData(string date, string name, string score)
    {
        WWWForm form = new WWWForm();
        form.AddField("DATE", date);
        form.AddField("NAME", name);
        form.AddField("SCORE", score);

        string urlPost = "https://demo1743076.mockable.io/match";

        WWW postRequest = new WWW(urlPost, form);
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
    public float age;
}

#endregion

#endregion
