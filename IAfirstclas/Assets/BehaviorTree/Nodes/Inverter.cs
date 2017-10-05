using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter<T> : BTOneChild<T> where T : class
{
    public Inverter(T blackboard) : base(blackboard){
    }

    override protected States Run()
    {
        if (_child.Update() == States.Done)
        {
            return States.Fail;
        }
        else if(_child.Update() == States.Fail)
        {
            return States.Done;
        }
        else if (_child.Update() == States.None)
        {
            return States.None;
        }
        return States.Running;
    }

    protected override void Awake()    {    }
    protected override void Reset()    {    }
    protected override void Sleep()    {    }
}
