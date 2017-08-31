using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Node _parent;
    public List<Node> adjNodes;

    public float cost;
    public float accumCost;
    public float _mineralAmount;

    private void Start()
    {
        _mineralAmount = Random.Range(50, 100);
        //cost = Random.Range(0, 5);
        for (int i = 0; i < NodeManager._instance._nodes.Count; i++)
        {
            if (NodeManager._instance._nodes[i] != this)
            {
            if (Vector3.Distance(transform.position, NodeManager._instance._nodes[i].transform.position) < 6)
            {
                if (!adjNodes.Contains(NodeManager._instance._nodes[i]))
                {
                    adjNodes.Add(NodeManager._instance._nodes[i]);
                }
            }
            }
        }
    }
}
