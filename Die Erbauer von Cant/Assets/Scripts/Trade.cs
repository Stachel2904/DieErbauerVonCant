using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Trade
{
    public string giver;
    public string taker;
    public string[] givenRessources;
    public string[] askedRessources;

    public Trade(string _giver, string _taker, string[] _givenRessources, string[] _askedRessources)
    {
        giver = _giver;
        taker = _taker;
        givenRessources = _givenRessources;
        askedRessources = _askedRessources;
    }
}

