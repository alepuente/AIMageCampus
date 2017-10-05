using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMine : BTNoChilds<BlackBoard> {

    BlackBoard _blackboard;
    public GoToMine(BlackBoard blackboard) : base(blackboard)
    {
        this._blackboard = blackboard;
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override States Run()
    {
        if (_blackboard._miner._pathFinder._path.Count == 0)
        {
            _blackboard._miner._pathFinder.FindPath(4);
            _blackboard._miner._nodeTrack = _blackboard._miner._pathFinder._path.Count - 1;
        }
        if (_blackboard._miner._pathFinder._path.Count > 0)
        {
            if (_blackboard._miner._nodeTrack >= 0)
            {
                _blackboard._miner.transform.position = Vector3.MoveTowards(_blackboard._miner.transform.position, _blackboard._miner._pathFinder._path[_blackboard._miner._nodeTrack].transform.position, _blackboard._miner._speed * Time.deltaTime);
                if (_blackboard._miner.gameObject.transform.position == _blackboard._miner._pathFinder._path[_blackboard._miner._nodeTrack].transform.position)
                {
                    _blackboard._miner._nodeTrack--;
                }
                return BTNode<BlackBoard>.States.Running;
            }
            else
            {
                return BTNode<BlackBoard>.States.Done;
            }
        }
        else
        {
            return BTNode<BlackBoard>.States.Fail;
        }
    }
    protected override void Sleep() { }
}
