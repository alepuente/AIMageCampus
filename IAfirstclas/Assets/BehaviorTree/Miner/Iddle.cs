using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iddle : BTNoChilds<BlackBoard>{

    BlackBoard _blackboard;
    public Iddle(BlackBoard blackboard) : base(blackboard)
    {
        this._blackboard = blackboard;
    }

    protected override void Awake()    {    }
    protected override void Reset()    {    }
    protected override States Run()
    {
        if (_blackboard._miner._pathFinder._nodeTarget._mineralAmount > 0)
        {
            return BTNode<BlackBoard>.States.Done;
        }
        else
        {
            _blackboard._miner._pathFinder.gameObject.transform.Rotate(_blackboard._miner._pathFinder.gameObject.transform.up, 10f * Time.deltaTime);
            return BTNode<BlackBoard>.States.Fail;
        }
    }
    protected override void Sleep()    {    }
}
