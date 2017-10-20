using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    private List<Boid> _boids;
    private float _weight;
    private Vector3 _alignment;
    private Vector3 _cohesion;
    private Vector3 _separation;

    public float _minDistance;
    public GameObject _objective;
    public float _cohesionV;
    public float _separationV;
    public float _alignmentV;
    public float _boidsSpeed;
    public float _boidsSteeringSpeed;
    public float _directionV;




    // Use this for initialization
    void Start()
    {
        _boids = new List<Boid>();
        foreach (Boid item in GetComponentsInChildren<Boid>())
        {
            _boids.Add(item);
            item._speed = _boidsSpeed;
            item._steeringSpeed = _boidsSteeringSpeed;
        }
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
            _boids[currentBoid]._lookObjective = _objective.transform;

            //Set boids neighboors
            for (int otherBoid = currentBoid + 1; otherBoid < _boids.Count; otherBoid++)
            {
                if (Vector3.Distance(_boids[currentBoid].transform.position, _boids[otherBoid].transform.position) < _minDistance)
                {
                    _boids[currentBoid]._neighbors.Add(_boids[otherBoid]);
                    _boids[otherBoid]._neighbors.Add(_boids[currentBoid]);
                }
            }


            //Ask each boid to check rules      
            //Weight Calculation
            Vector3 center = CalculateCenterOfGroup(_boids[currentBoid]);

            _weight = Vector3.Distance(_boids[currentBoid].transform.position, center) / _minDistance;

            //Cohesion Calculation
            _cohesion = (center * _cohesionV - _boids[currentBoid].transform.position) * _weight;
            _cohesion.Normalize();

            //Separation Calculation
            _separation = (_boids[currentBoid].transform.position - center * _separationV) * (1 - _weight);
            _separation.Normalize();

            //Aligment Calculation
            _alignment = CalculateAlignment(_boids[currentBoid]);

            //Calculate de vector result
            Vector3 result = (_cohesion + _separation + _alignment) / 3;
            result.Normalize();

            //Setting the boid vector
            _boids[currentBoid]._direction = (result + (_objective.transform.position - _boids[currentBoid].transform.position).normalized).normalized;

            //Reset vectors and Neighboors
            _alignment = Vector3.zero;
            _cohesion = Vector3.zero;
            _separation = Vector3.zero;
            _boids[currentBoid]._neighbors.Clear();
        }
    }

    public Vector3 CalculateCenterOfGroup(Boid boid)
    {
        Vector3 aux = boid.transform.position;
        for (int i = 0; i < boid._neighbors.Count; i++)
        {
            aux += boid._neighbors[i].transform.position;
        }
        if (boid._neighbors.Count > 0)
        {
            aux /= boid._neighbors.Count + 1;
        }
        return aux;
    }   

    public Vector3 CalculateAlignment(Boid boid)
    {
        Vector3 aux = boid.transform.forward;
        for (int i = 0; i < boid._neighbors.Count; i++)
        {
            aux += boid._neighbors[i].transform.forward;
        }
        if (boid._neighbors.Count > 0)
        {
            aux /= boid._neighbors.Count + 1;
        }
        aux *= _alignmentV;
        return aux.normalized;
    }

}
