using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTOneChild : BTNode {

    protected BTNode _child;

    public override bool CanHaveChilds()
    {
       return true;
    }

    public void AddChild(BTNode node)
    {
        _child = node;
    }

    public override States Run()
    {
        return States.None;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
