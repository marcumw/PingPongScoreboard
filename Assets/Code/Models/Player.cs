using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class Player {

    private int _id;
    private float _age;
    private string _name;
    private List<Match> _matches = new List<Match>();

    private int _wins = 0;
    private int _losses = 0;
    private float _ratio = 0;

    public Player(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public static void setWinsAndLosses(Player player)
    {
        foreach (Match match in player._matches)
        {
            if (match.WinnerId == player.Id)
            {
                player._wins++;
            }
        }

        player._losses = player._matches.Count - player._wins;

        if (player.Wins > 0 && player.Losses > 0)
        {
            player._ratio = (float)player.Wins / (float)player._matches.Count;
        }

    }

    public string Name {
        get
        {
            return _name;
        }
    }

    public float Age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
        }
    }

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public int Wins
    {
        get
        {
            return _wins;
        }
    }

    public int Losses
    {
        get
        {
            return _losses;
        }
    }

    public float Ratio
    {
        get
        {
            return _ratio;
        }
    }

    public List<Match> Matches
    {
        get
        {
            return _matches;
        }

        set
        {
            _matches = value.OrderByDescending(m => m.Date).ToList();


        }
    }
}
