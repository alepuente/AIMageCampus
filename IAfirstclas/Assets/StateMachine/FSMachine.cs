using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FSMachine {

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

    // Use this for initialization
    public FSMachine () {
        _fsm = new int[rows, columns];
        resetFSM();
        SetRelative(States.Iddle,Events.MineNotEmpty,States.GoingToMine);
        SetRelative(States.GoingToMine, Events.ArrivedToMine, States.Mining);
        SetRelative(States.Mining, Events.MineEmpty, States.GoingToHouse);
        SetRelative(States.Mining, Events.ImFull, States.GoingToHouse);
        SetRelative(States.GoingToHouse, Events.ArrivedToHouse, States.Depositing);
        SetRelative(States.Depositing, Events.ImEmpty, States.Iddle);
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

    private void SetRelative(States origin, Events evnt, States dest)
    {
        _fsm[(int)origin, (int)evnt] = (int)dest;
    }    

    public void SetEvent(Events ev)
    {
        Debug.Log(ev);
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
