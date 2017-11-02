using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorith : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Chromosome> CrossOut(List<Ship> population)
    {
        float total = 0;
        List<Chromosome> aux = new List<Chromosome>();
        foreach (Ship item in population)
        {
            total += item._score;
        }
        for (int current = population.Count - 1; current >= 0; current--)
        {
            for (int other = population.Count - 1; other >= 0; other--)
            {
                if (Random.Range(0,total) <= population[current]._score + population[other]._score)
                {
                    if (aux.Count == population.Count)
                    {
                        break;
                    }
                    else
                    {
                        Chromosome[] pair = MixChromosomes(population[current]._genome, population[other]._genome);
                        aux.Add(pair[0]);
                        aux.Add(pair[1]);
                        other = 0;
                    }
                }
            }
        }
        return aux;
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
