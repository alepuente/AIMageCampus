using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : BTNoChilds<BlackBoard> {

    BlackBoard _blackboard;
    public Deposit(BlackBoard blackboard) : base(blackboard)
    {
        this._blackboard = blackboard;
    }

    protected override void Awake() { }
    protected override void Reset() { }
    protected override States Run()
    {
        if (_blackboard._miner._loadAmount > 0)
        {
            _blackboard._miner._loadAmount -= 10f * Time.deltaTime;
            _blackboard._miner.totalLoad += 10f * Time.deltaTime;
            return BTNode<BlackBoard>.States.Running;
        }
        else
        {
            return BTNode<BlackBoard>.States.Done;
        }
    }
    protected override void Sleep() { }
}
