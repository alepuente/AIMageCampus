using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : BTOneChild {

    public override States Run()
    {
        if (_child.Run() == States.Done)
        {
            return States.Fail;
        }
        else if(_child.Run() == States.Fail)
        {
            return States.Done;
        }
        return States.Running;
    }
}
