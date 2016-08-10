using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public sealed class Match {

    private int _id;
    private DateTime _date;
    private List<PlayerScore> _playerScores;

    public Match(int id, string date, List<PlayerScore> playerScores)
    {
        _id = id;
        _date = Convert.ToDateTime(date);
        _playerScores = playerScores;
    }
}

public sealed class PlayerScore
{
    private int _id;
    private int _score;

    public PlayerScore(int id, int score)
    {
        _id = id;
        _score = score;
    }
}
