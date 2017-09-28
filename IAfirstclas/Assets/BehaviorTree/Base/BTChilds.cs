using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChilds : BTNode {

    public List<BTNode> _childs;


    public override bool CanHaveChilds()
    {
        return true;
    }

    public override States Run()
    {
        return States.None;
    }

    public bool AddChild(BTNode node)
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

    // Use this for initialization
    void Start () {
        _childs = new List<BTNode>();
	}
	
	void Update () {
		
	}
}
