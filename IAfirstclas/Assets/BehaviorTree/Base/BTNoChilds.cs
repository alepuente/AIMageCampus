using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNoChilds : BTNode {

    public override bool CanHaveChilds()
    {
        return false;
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
