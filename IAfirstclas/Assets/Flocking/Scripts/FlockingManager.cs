using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    public List<Boid> _boids;
    public float _minDistance;
    public GameObject _objective;
    public float _weight;
    private Vector3 _alignment;
    private Vector3 _cohesion;
    private Vector3 _separation;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateBoids();
    }

    public void UpdateBoids()
    {
        for (int currentBoid = 0; currentBoid < _boids.Count; currentBoid++)
        {
            _alignment += _boids[currentBoid].transform.forward;
            _boids[currentBoid]._lookObjective = _objective.transform;

            //Set boids neighboors
            for (int otherBoid = currentBoid + 1; otherBoid < _boids.Count; otherBoid++)
            {
                if (Vector3.Distance(_boids[currentBoid].transform.position, _boids[otherBoid].transform.position) < _minDistance)
                {
                    _alignment += _boids[otherBoid].transform.forward;

                    _boids[currentBoid]._neighbors.Add(_boids[otherBoid]);
                    _boids[otherBoid]._neighbors.Add(_boids[currentBoid]);
                }
            }


            //Ask each boid to check rules      
            _weight = Vector3.Distance(_boids[currentBoid].transform.position, CenterOfNeighboors(_boids[currentBoid])) / _minDistance;
            _cohesion = (CenterOfNeighboors(_boids[currentBoid]) - _boids[currentBoid].transform.position) * _weight;
            _cohesion.Normalize();
            _separation = (_boids[currentBoid].transform.position - CenterOfNeighboors(_boids[currentBoid])) * (1 - _weight);
            _separation.Normalize();
            if (_boids[currentBoid]._neighbors.Count > 0)
            {
                _alignment /= _boids[currentBoid]._neighbors.Count + 1;
                _alignment.Normalize();
            }
            Vector3 result = (_cohesion + _separation + _alignment) / 3;
            result.Normalize();
            _boids[currentBoid]._direction = (result + (_objective.transform.position - _boids[currentBoid].transform.position).normalized).normalized;
            _alignment = Vector3.zero;
            _cohesion = Vector3.zero;
            _separation = Vector3.zero;
            _boids[currentBoid]._neighbors.Clear();
        }
        /*for (int i = 0; i < _boids.Count; i++)
        {
            _boids[i]._neighbors.Clear();
        }*/
    }

    public Vector3 CenterOfNeighboors(Boid boid)
    {
        Vector3 aux = Vector3.zero;
        for (int i = 0; i < boid._neighbors.Count; i++)
        {
            aux = aux + boid._neighbors[i].transform.position;
        }
        if (boid._neighbors.Count > 0)
        {
            aux /= boid._neighbors.Count;
        }
        return aux.normalized;
    }

}
