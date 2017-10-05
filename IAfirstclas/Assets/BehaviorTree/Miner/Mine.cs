using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : BTNoChilds<BlackBoard>
{

    BlackBoard _blackboard;
    public Mine(BlackBoard blackboard) : base(blackboard)
    {
        this._blackboard = blackboard;
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override States Run()
    {
        if (_blackboard._miner._pathFinder._nodeTarget._mineralAmount > 0)
        {
            if (_blackboard._miner._loadAmount <= _blackboard._miner._maxLoad)
            {
                _blackboard._miner._loadAmount += 10f * Time.deltaTime;
                _blackboard._miner._pathFinder._nodeTarget._mineralAmount -= 10f * Time.deltaTime;
                return BTNode<BlackBoard>.States.Running;
            }
            else
            {
                _blackboard._miner._nodeTrack = 0;
                return BTNode<BlackBoard>.States.Done;
            }
        }
        else
        {
            _blackboard._miner._nodeTrack = 0;
            return BTNode<BlackBoard>.States.Done;
        }
    }
    protected override void Sleep() { }
}
