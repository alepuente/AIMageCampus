using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNChromosome {

    public List<Gen> _chromosome;
    public NNChromosome()
    {
        _chromosome = new List<Gen>();
    }
    public NNChromosome(int maxActions , List<float> weights)
    {
        _chromosome = new List<Gen>();
        for (int i = 0; i < maxActions; i++)
        {
            _chromosome.Add(new Gen(weights[i]));
        }
    }

    public struct Gen
    {
        public float _weight;
        public Gen(float weight)
        {
            _weight = weight;
        }
    }

    


}
