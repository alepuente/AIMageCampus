using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    public Node _nodeTarget;
    public Node _startingNode;

    public Queue<Node> _openNodes;
    public Queue<Node> _closedNodes;

    public Stack<Node> _openNodesStack;

    public List<Node> _openNodesList;

    public List<Node> _path;
    private Node _currentNode;
    public int _searchType;

    public static PathFinder _instance;

    // Use this for initialization
    void Start()
    {
        _instance = this;
        _openNodes = new Queue<Node>();
        _closedNodes = new Queue<Node>();
        _openNodesStack = new Stack<Node>();        

        _path = new List<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButtonDown (0))
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Node")
                {
                    _startingNode = hit.transform.GetComponent<Node>();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Node")
                {
                    _nodeTarget = hit.transform.GetComponent<Node>();
                }
            }
        }        
    }

    public void FindPath(int searchType)
    {
        _openNodes.Clear();
        _closedNodes.Clear();
        _openNodesStack.Clear();
        _openNodesList.Clear();
        _path.Clear();
        _openNodes.Enqueue(_startingNode);
        _openNodesStack.Push(_startingNode);
        _openNodesList.Add(_startingNode);
        switch (searchType)
        {
            case 1: VisitNodeBreadth(_openNodes.Peek());
                break;
            case 2: VisitNodeDepth(_openNodesStack.Peek());
                break;
            case 3: VisitNodeDijkstra(_openNodesList[0]);
                break;
            case 4: VisitNodeStar(_openNodesList[0]);
                break;
            default:
                break;
        }
    }

    void VisitNodeBreadth(Node node)
    {
        if (node != _nodeTarget)
        {
            for (int i = 0; i < node.adjNodes.Count; i++)
            {                
                if (!_closedNodes.Contains(node.adjNodes[i]))
                {
                    _openNodes.Enqueue(node.adjNodes[i]);
                    node.adjNodes[i]._parent = node;
                }                
            }
            _closedNodes.Enqueue(node);
            _openNodes.Dequeue();
            VisitNodeBreadth(_openNodes.Peek());
        }
        else
        {
            ReturnPath();
        }
    }

    void VisitNodeDepth(Node node)
    {
        if (node != _nodeTarget)
        {
            _openNodesStack.Pop();
            for (int i = 0; i < node.adjNodes.Count; i++)
            {
                if (!_closedNodes.Contains(node.adjNodes[i]))
                {
                    _openNodesStack.Push(node.adjNodes[i]);
                    node.adjNodes[i]._parent = node;
                }
            }
            _closedNodes.Enqueue(node);
            VisitNodeDepth(_openNodesStack.Peek());
        }
        else
        {
            ReturnPath();
        }
    }

    void VisitNodeDijkstra(Node node)
    {
        if (node != _nodeTarget)
        {
            for (int i = 0; i < node.adjNodes.Count; i++)
            {
                if (_openNodesList.Contains(node.adjNodes[i]))
                {
                    if (node.accumCost < node.adjNodes[i]._parent.accumCost)
                    {
                        node.adjNodes[i]._parent = node;
                        node.adjNodes[i].accumCost = node.accumCost + node.adjNodes[i].cost;
                    }
                }
                else if (!_closedNodes.Contains(node.adjNodes[i]))
                {
                    _openNodesList.Add(node.adjNodes[i]);
                    node.adjNodes[i]._parent = node;
                    node.adjNodes[i].accumCost = node.accumCost + node.adjNodes[i].cost;
                }
            }
            _closedNodes.Enqueue(node);
            _openNodesList.Remove(node);
            Node aux;
            aux = _openNodesList[0];
            for (int i = 0; i < _openNodesList.Count; i++)
            {
                if (_openNodesList[i].accumCost < aux.accumCost)
                {
                    aux = _openNodesList[i];
                }
            }
            VisitNodeDijkstra(aux);
        }
        else
        {
            ReturnPath();
        }
    }

    void VisitNodeStar(Node node)
    {
        if (node != _nodeTarget)
        {
            for (int i = 0; i < node.adjNodes.Count; i++)
            {
                if (_openNodesList.Contains(node.adjNodes[i]))
                {
                    if (node.accumCost < node.adjNodes[i]._parent.accumCost)
                    {
                        node.adjNodes[i]._parent = node;
                        node.adjNodes[i].accumCost = node.accumCost + node.adjNodes[i].cost;
                    }
                }
                else if (!_closedNodes.Contains(node.adjNodes[i]))
                {
                    _openNodesList.Add(node.adjNodes[i]);
                    node.adjNodes[i]._parent = node;
                    node.adjNodes[i].accumCost = node.accumCost + node.adjNodes[i].cost;
                }
            }
            _closedNodes.Enqueue(node);
            _openNodesList.Remove(node);
            Node aux;
            aux = _openNodesList[0];
            for (int i = 0; i < _openNodesList.Count; i++)
            {
                if ((_openNodesList[i].accumCost + Vector3.Distance(_openNodesList[i].gameObject.transform.position,_nodeTarget.gameObject.transform.position)) < (aux.accumCost + Vector3.Distance(aux.gameObject.transform.position, _nodeTarget.gameObject.transform.position)))
                {
                    aux = _openNodesList[i];
                }
            }
            VisitNodeStar(aux);
        }
        else
        {
            ReturnPath();
        }
    }


    void ReturnPath()
    {
        _currentNode = _nodeTarget;
        _path.Add(_nodeTarget);
        while (_currentNode._parent != _startingNode)
        {
            _path.Add(_currentNode._parent);
            _currentNode = _currentNode._parent;
        }
        _path.Add(_startingNode);
    }



}
