using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour {
    public List<Ship> _population;
    public GeneticAlgorith _genManager;
    public GameObject _citizenPrefab;
    public int _totalPopulation;
    public float _testTime = 10;
    public GameObject _target;
    public int _maxActions = 10;
    public int _eliteAmount = 2;
    public float _distanceModifier;
    public float _flyingModifier;
    public float _hitModifier;
    public float _mutationRate;
    public float _mutationValue;

    private bool _onTest;
    private Chromosome _chromoManager;
    private float _timer = 0;

	// Use this for initialization
	void Start () {
        _chromoManager = new Chromosome();
        _genManager = new GeneticAlgorith();
        _chromoManager._maxActions = _maxActions;//change this, no need to creat one on each ship
        for (int i = 0; i < _totalPopulation; i++)
        {
            GameObject aux = Instantiate(_citizenPrefab);
            Ship newShip = aux.GetComponent<Ship>();
            _chromoManager.CreateRandomChromosome(ref newShip);
            newShip._target = _target;
            newShip._distanceModifier = _distanceModifier;
            newShip._hitModifier = _hitModifier;
            newShip._flyingModifier = _flyingModifier;
            newShip.gameObject.SetActive(false);
            _population.Add(newShip);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!_onTest)
        {
            TestCitizens();
        }
        if (_onTest)
        {
            _timer += Time.deltaTime;
            if (_timer > _testTime)
            {
                CheckCitizensScores();
                List<Chromosome> aux = _genManager.CrossOut(_population);
                for (int i = 0; i < _population.Count - _eliteAmount; i++)
                {
                    for (int x = 0; x < aux[i]._chromosome.Count; x++)
                    {
                        if (Random.Range(0f,2f) < _mutationRate)
                        {
                            aux[i]._chromosome[x]._time += Random.Range(-_mutationValue,_mutationValue);
                        }
                    }
                    _population[i]._genome = aux[i];
                }
                _onTest = false;
                _timer = 0;
            }
        }
	}
    private void CheckCitizensScores()
    {
        foreach (Ship item in _population)
        {
            item.CheckScore();
            item.ResetPos();
            item._timer = 0;
            item.gameObject.SetActive(false);
        }
        _population.Sort(delegate (Ship a, Ship b)
        {
            return (a._score).CompareTo(b._score);
        });       
    }

    private void TestCitizens()
    {
        foreach (Ship item in _population)
        {
            item.gameObject.SetActive(true);
        }
        _onTest = true;
    }
    
}
