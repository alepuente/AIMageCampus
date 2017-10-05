using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNoChilds<T> : BTNode<T> where T : class
{
    protected BTNoChilds(T blackboard) : base(blackboard){ }

    public override bool CanHaveChilds()
    {
        return false;
    }
   
}
