using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : BTChilds
{
    int index;

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
                    index++;
                    break;
                case States.Running:
                    return States.Running;
                case States.Fail:
                    Reset();
                    return States.Fail;
                default:
                    break;
            }
            if (index > _childs.Count - 1)
            {
                Reset();
                return States.Done;
            }
        }
        return States.Fail;
    }
}
