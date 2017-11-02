using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{

    public Ship.Action _action;
    public float _time;

    public Gen(ref Ship ship)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                _action = ship.ApplyTruster;
                break;
            case 1:
                _action = ship.RotateLeft;
                break;
            case 2:
                _action = ship.RotateRight;
                break;
            case 3:
                _action = ship.DoNothing;
                break;
            default:
                break;
        }
        _time = Random.Range(0, 3);
    }


}
