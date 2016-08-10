using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Player {

    private int _id;
    private int _age;
    private string _name;
    public List<Match> _matches = new List<Match>();

    public Player(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public string Name {
        get
        {
            return _name;
        }
    }

    public int Age
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
}
