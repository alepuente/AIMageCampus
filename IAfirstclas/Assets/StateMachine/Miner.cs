using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {
    public int _loadAmount;
    public int _maxLoad;
    public PathFinder _pathFinder;
	// Use this for initialization
	void Start () {
        _pathFinder = GetComponent<PathFinder>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (_actualState)
        {
            case 1: GoToMine();
                break;
            default:
                break;
        }
    }

    private void GoToMine()
    {
        _pathFinder._isSearching = true;
    }

    
}
