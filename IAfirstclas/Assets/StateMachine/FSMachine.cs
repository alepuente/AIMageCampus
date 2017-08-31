using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FSMachine : MonoBehaviour {

    private int[,] _fsm;
    private States _actualState;
    private int rows = 5;
    private int columns = 6;

    public enum States
    {
        Iddle,
        GoingToMine,
        Mining,
        GoingToHouse,
        Depositing
    }
    public enum Events
    {
        ArrivedToMine,
        ImFull,
        ArrivedToHouse,
        MineEmpty,
        ImEmpty,
        MineNotEmpty
    }
    public enum Destiny
    {
        GoToMine,
        GoToHouse,
        Mine,
        Deposit,
        GoIddle
    }


    // Use this for initialization
    void Start () {      
        _fsm = new int[rows, columns];
        resetFSM();
        SetRelative(States.Iddle,Events.MineNotEmpty,Destiny.GoToMine);
        SetRelative(States.GoingToMine, Events.ArrivedToMine, Destiny.Mine);
        SetRelative(States.Mining, Events.ImEmpty, Destiny.GoToHouse);
        SetRelative(States.GoingToHouse, Events.ArrivedToHouse, Destiny.Deposit);
        SetRelative(States.Depositing, Events.ImEmpty, Destiny.GoIddle);
        _actualState = States.Iddle;
    }

    private void resetFSM()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                _fsm[r, c] = -1;
            }
        }
    }

    private void SetRelative(States origin, Events evnt, Destiny dest)
    {
        _fsm[(int)origin, (int)evnt] = (int)dest;
    }    

    public void SetEvent(Events ev)
    {
        if (_fsm[(int)_actualState, (int)ev] != -1)
        {
           _actualState = (States)_fsm[(int)_actualState, (int)ev];
        }
    }

    public States getActualState()
    {
        return _actualState;
    }
}
