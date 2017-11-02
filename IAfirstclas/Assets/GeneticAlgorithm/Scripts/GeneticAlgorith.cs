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
        int newShipCount = 0;
        List<Chromosome> aux = new List<Chromosome>();
        foreach (Ship item in population)
        {
            total += item._score;
        }
        for (int current = population.Count -1; current > 0; current--)
        {
            for (int other = population.Count - 1; other > 0; other--)
            {
                if (Random.Range(0,total) <= population[current]._score + population[other]._score)
                {
                    KeyValuePair<Chromosome, Chromosome> pair = MixChromosomes(population[current]._genome, population[other]._genome);
                    aux.Add(pair.Key);
                    aux.Add(pair.Value);
                    break; 
                }
            }
        }
        return aux;
    }

    public KeyValuePair<Chromosome, Chromosome> MixChromosomes(Chromosome a, Chromosome b)
    {
        KeyValuePair<Chromosome, Chromosome> result = new KeyValuePair<Chromosome, Chromosome>();
        for (int i = 0; i < a._chromosome.Count/2; i++)
        {
            result.Key = a._chromosome[i];//This is the error
        }
        for (int i = a._chromosome.Count / 2 - 1; i > 0; i--)
        {
            result.Key._chromosome.Add(b._chromosome[i]);
        }

        for (int i = 0; i < a._chromosome.Count / 2; i++)
        {
            result.Value._chromosome.Add(b._chromosome[i]);
        }
        for (int i = a._chromosome.Count / 2 - 1; i > 0; i--)
        {
            result.Value._chromosome.Add(a._chromosome[i]);
        }
        return result;
    }
}
