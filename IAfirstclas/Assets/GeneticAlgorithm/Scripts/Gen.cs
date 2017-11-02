using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{
    public enum Actions
    {
        ApplyTruster,
        RotateLeft,
        RotateRight,
        DoNothing
    }

    public Actions _action;
    public float _time;

    public Gen(ref Ship ship)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                _action = Actions.ApplyTruster;
                break;
            case 1:
                if (Random.Range(0,2) == 0)
                {
                _action = Actions.RotateLeft;
                }
                else
                {
                _action = Actions.RotateRight;
                }            
                break;
            case 2:
                _action = Actions.DoNothing;
                break;
            default:
                break;
        }
        _time = Random.Range(0f, 4f);
    }


}
