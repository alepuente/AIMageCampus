using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNChromosome {

    public List<Gen> _chromosome;
    public NNChromosome()
    {
        _chromosome = new List<Gen>();
    }
    public NNChromosome(int maxActions)
    {
        _chromosome = new List<Gen>();
        for (int i = 0; i < maxActions; i++)
        {
            _chromosome.Add(CreateRandomGen());
        }
    }

    public struct Gen
    {
        public Actions _action;
        public float _time;
    }
    public enum Actions
    {
        ApplyTruster,
        RotateLeft,
        RotateRight,
        DoNothing
    }

    public Gen CreateRandomGen()
    {
        Gen _gen = new Gen();
        switch (Random.Range(0, 3))
        {
            case 0:
                _gen._action = Actions.ApplyTruster;
                break;
            case 1:
                if (Random.Range(0, 2) == 0)
                {
                    _gen._action = Actions.RotateLeft;
                }
                else
                {
                    _gen._action = Actions.RotateRight;
                }
                break;
            case 2:
                _gen._action = Actions.DoNothing;
                break;
            default:
                break;
        }
        _gen._time = Random.Range(0f, 4f);
        return _gen;
    }


}
