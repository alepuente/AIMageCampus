using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector<T> : BTChilds<T> where T : class
{
    int index;
    public Selector(T blackboard) : base(blackboard){
        index = 0;
    }
    override protected States Run()
    {
        if (_childs.Count > 0)
        {
            if (index > _childs.Count - 1)
            {
                Reset();
                return States.Fail;
            }
            switch (_childs[index].Update())
            {
                case States.Done:
                    Reset();
                    return States.Done;
                case States.Running:
                    return States.Running;
                case States.Fail:
                    index++;
                    break;
                default:
                    break;
            }
        }
        return States.Fail;
    }

    protected override void Sleep()    {    }
    protected override void Awake()    {    }
    protected override void Reset()    {  index = 0;    }
}
