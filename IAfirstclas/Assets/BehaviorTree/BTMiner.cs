using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMiner : MonoBehaviour
{

    public PathFinder _pathFinder;

    public float _loadAmount;
    public float _maxLoad;
    public float _speed;
    private float _mineTimer;
    public float totalLoad;

    public int _nodeTrack;
    private Sequencer<BlackBoard> _root;
    private BTBuilder builder;

    BlackBoard blackboard;
    // Use this for initialization
    void Start()
    {
        blackboard = new BlackBoard();
        blackboard._miner = this;

        _pathFinder = GetComponent<PathFinder>();
        builder = new BTBuilder();
        _root = builder.BuildTree(blackboard);
    }

    // Update is called once per frame
    void Update()
    {
        UIManager._instance._goldAm.text = "Total Gold: " + ((int)totalLoad).ToString();
        UIManager._instance._minerAm.text = "Miner Gold: " + ((int)_loadAmount).ToString();
        if (_pathFinder._nodeTarget != null && _pathFinder._startingNode != null)
        {
            _root.Update();
        }
    }
}
