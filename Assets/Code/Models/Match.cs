using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public struct Match {

    private int _id;
    private DateTime _date;
    private List<MatchScore> _playerScores;
    private int _winnerId;

    public Match(int id, string date, List<MatchScore> matchScores)
    {
        _id = id;
        _date = Convert.ToDateTime(date);
        _playerScores = matchScores;

        _winnerId = _playerScores.OrderByDescending(ps => ps.Score).First().PlayerId;
    }

    public string Id
    {
        get
        {
            return _id.ToString();
        }
    }

    public DateTime Date
    {
        get
        {
            return _date;
        }
    }

    public int WinnerId
    {
        get
        {
            return _winnerId;
        }
    }
}
