using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour
{
    public List<Gen> _chromosome;
    public Chromosome()
    {
        _chromosome = new List<Gen>();
    }
    public int _maxActions;

    public void CreateRandomChromosome(ref Ship ship)
    {
        for (int i = 0; i < _maxActions; i++)
        {
            ship._genome._chromosome.Add(new Gen(ref ship));
        }        
    }
 
}
