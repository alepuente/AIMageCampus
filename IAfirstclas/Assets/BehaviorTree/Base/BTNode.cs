using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public enum States
    {
        Done,
        Running,
        Fail,
        None
    }


    public abstract bool CanHaveChilds();
    public abstract States Run();
    public States _state;
}
