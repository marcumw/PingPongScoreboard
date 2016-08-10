using UnityEngine;
using System.Collections;

public sealed class MatchScore
{
    private int _playerId;
    private int _score;

    public MatchScore(int id, int score)
    {
        _playerId = id;
        _score = score;
    }

    public int PlayerId
    {
        get
        {
            return _playerId;
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
    }
}
