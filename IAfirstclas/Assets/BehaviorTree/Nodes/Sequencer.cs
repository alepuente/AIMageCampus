using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer<T> : BTChilds<T> where T : class
{
    int index;

    public Sequencer(T blackboard) : base(blackboard){
        index = 0;
    } 

    override protected States Run()
    {             
        if (_childs.Count > 0)
        {
            if (index >= _childs.Count)
            {
                Reset();
                return States.Done;
            }
            switch (_childs[index].Update())
            {
                case States.Done:
                    index++;
                    return States.Running;
                    break;
                case States.Running:
                    return States.Running;
                case States.Fail:
                    Reset();
                    return States.Fail;
                default:
                    return BTNode<T>.States.Fail;
            }           
        }
        else
        {
            return BTNode<T>.States.Fail;
        }
    }

    protected override void Reset()    {index = 0;    }

    protected override void Awake()    {    }

    protected override void Sleep()    {    }
}
