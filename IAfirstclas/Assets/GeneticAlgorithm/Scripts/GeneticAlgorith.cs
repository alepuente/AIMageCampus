using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorith {

    public List<Chromosome> CrossOut(List<Ship> oldPopulation)
    {
        float total = 0;
        List<Chromosome> _newPopulation = new List<Chromosome>();
        foreach (Ship item in oldPopulation)
        {
            total += item._score;
        }
        for (int current = oldPopulation.Count - 1; current >= 0; current--)
        {
            for (int other = oldPopulation.Count - 1; other >= 0; other--)
            {
                if (Random.Range(0,total) <= oldPopulation[current]._score + oldPopulation[other]._score)
                {
                    if (_newPopulation.Count == oldPopulation.Count)
                    {
                        break;
                    }
                    else
                    {
                        Chromosome[] pair = MixChromosomes(oldPopulation[current]._genome, oldPopulation[other]._genome);
                        _newPopulation.Add(pair[0]);
                        _newPopulation.Add(pair[1]);
                        other = 0;
                    }
                }
            }
        }
        return _newPopulation;
    }

    public Chromosome[] MixChromosomes(Chromosome a, Chromosome b)
    {
        Chromosome[] result = new Chromosome[2];
        result[0] = new Chromosome();
        result[1] = new Chromosome();

        for (int i = 0; i < a._chromosome.Count/2; i++)
        {
            result[0]._chromosome.Add(a._chromosome[i]);
        }
        for (int i = a._chromosome.Count / 2; i < a._chromosome.Count; i++)
        {
            result[0]._chromosome.Add(b._chromosome[i]);
        }

        for (int i = 0; i < a._chromosome.Count / 2; i++)
        {
            result[1]._chromosome.Add(b._chromosome[i]);
        }
        for (int i = a._chromosome.Count / 2; i < a._chromosome.Count; i++)
        {
            result[1]._chromosome.Add(a._chromosome[i]);
        }
        return result;
    }
}
