using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTChilds<T> : BTNode<T> where T : class
{

    public List<BTNode<T>> _childs;

    public BTChilds(T blackboard) : base(blackboard)
    {
        _childs = new List<BTNode<T>>();
    }


    public override bool CanHaveChilds()
    {
        return true;
    }

    public bool AddChild(BTNode<T> node)
    {
        if (_childs.Contains(node))
        {
            return false;
        }
        else
        {
            _childs.Add(node);
            return true;
        }
    }

}
