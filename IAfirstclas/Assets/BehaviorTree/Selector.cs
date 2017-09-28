using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BTChilds
{
    int index;
    // Use this for initialization
    void Start()
    {
        Reset();
    }

    void Reset()
    {
        index = 0;
    }

    public override States Run()
    {
        if (_childs.Count > 0)
        {
            switch (_childs[index]._state)
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
            if (index > _childs.Count - 1)
            {
                Reset();
                return States.Fail;
            }
        }
        return States.Fail;
    }
}
