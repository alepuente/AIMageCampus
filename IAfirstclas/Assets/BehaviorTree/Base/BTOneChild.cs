using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTOneChild<T> : BTNode<T> where T : class
{

    protected BTNode<T> _child;

    public BTOneChild(T blackboard) : base(blackboard)
    {
    }

    public override bool CanHaveChilds()
    {
       return true;
    }

    public void AddChild(BTNode<T> node)
    {
        _child = node;
    }
  
}
