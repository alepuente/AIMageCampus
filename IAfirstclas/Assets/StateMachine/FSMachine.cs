using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FSMachine : MonoBehaviour {

    public static FSMachine _instance;
    public Miner[] _miners;
    public Deployed _deployed;
    public int[,] _fsm;
    public int _actualState;

    [System.Serializable]
    public class Deployed : UnityEvent<int>
    {
    }

    // Use this for initialization
    void Start () {
        _deployed = new Deployed();       
        _deployed.AddListener(SetEvent);
    }
    

    void SetEvent(int ev)
    {
        if (_fsm[_actualState, ev] != -1)
        {
            _actualState = _fsm[_actualState, ev];
        }
    }
	

	void Update () {
		
	}
}
