using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {
    public GameObject _node;
    public int weight;
    public int height;
    public int distance;
    public List<Node> _nodes;

    public static NodeManager _instance;

	// Use this for initialization
	void Start () {
        _instance = this;
      
        foreach (Node item in GetComponentsInChildren<Node>())
        {
            _nodes.Add(item);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
