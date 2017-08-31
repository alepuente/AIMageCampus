using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public PathFinder _pathFinder;
    private FSMachine _stateMachine;

    public float _loadAmount;
    public float _maxLoad;
    public float _speed;
    private float _mineTimer;

    private int _nodeTrack;

    // Use this for initialization
    void Start()
    {
        _pathFinder = GetComponent<PathFinder>();
        _stateMachine = new FSMachine();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_pathFinder._nodeTarget != null && _pathFinder._startingNode != null)
        {            
        switch (_stateMachine.getActualState())
        {
            case FSMachine.States.Iddle: Iddle();
                break;
            case FSMachine.States.GoingToMine: GoToMine();
                break;
            case FSMachine.States.Mining: Mine();
                break;
            case FSMachine.States.GoingToHouse: GoToHouse();
                break;
            case FSMachine.States.Depositing: Deposit();
                break;
            default:
                break;
        }
        }
    }
    private void GoToMine()
    {
        if (_pathFinder._path.Count == 0)
        {
            _pathFinder.FindPath(4);
            _nodeTrack = _pathFinder._path.Count - 1;
        }
        if (_pathFinder._path.Count > 0)
        {
            if (_nodeTrack >= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pathFinder._path[_nodeTrack].transform.position, _speed * Time.deltaTime);
                if (gameObject.transform.position == _pathFinder._path[_nodeTrack].transform.position)
                {
                    _nodeTrack--;
                }
            }
            else
            {
                _stateMachine.SetEvent(FSMachine.Events.ArrivedToMine);
            }           
        }
    }

    private void GoToHouse()
    {                  
        if (_pathFinder._path.Count > 0)
        {
            if (_nodeTrack < _pathFinder._path.Count)
            {
                transform.position = Vector3.MoveTowards(transform.position, _pathFinder._path[_nodeTrack].transform.position, _speed * Time.deltaTime);
                if (gameObject.transform.position == _pathFinder._path[_nodeTrack].transform.position)
                {
                    _nodeTrack++;
                }
            }
            else
            {
                _stateMachine.SetEvent(FSMachine.Events.ArrivedToHouse);
                _pathFinder._path.Clear();
            }
        }
    }

    private void Deposit()
    {
        if (_loadAmount > 0)
        {
            _loadAmount -= 10f * Time.deltaTime;
        }
        else
        {
            _stateMachine.SetEvent(FSMachine.Events.ImEmpty);
        }
    }

    private void Mine()
    {
        if (_pathFinder._nodeTarget._mineralAmount > 0)
        {
            if (_loadAmount <= _maxLoad)
            {
                _loadAmount += 10f * Time.deltaTime;
                _pathFinder._nodeTarget._mineralAmount -= 10f * Time.deltaTime;
            }
            else
            {
                _stateMachine.SetEvent(FSMachine.Events.ImFull);
                _nodeTrack = 0;
            }
        }
        else
        {
            _stateMachine.SetEvent(FSMachine.Events.MineEmpty);
            _nodeTrack = 0;
        }
    }

    private void Iddle()
    {
        if (_pathFinder._nodeTarget._mineralAmount > 0)
        {
            _stateMachine.SetEvent(FSMachine.Events.MineNotEmpty);
        }
        else
        {
            _pathFinder.gameObject.transform.Rotate(_pathFinder.gameObject.transform.up, 10f * Time.deltaTime);
        }
    }

}
