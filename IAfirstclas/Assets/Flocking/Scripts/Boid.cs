using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public Vector3 _direction;
    public List<Boid> _neighbors;
    public float _speed;
    public Transform _lookObjective;
    public float _steeringSpeed;

	// Use this for initialization
	void Start () {
        _neighbors = new List<Boid>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(_direction * _speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lookObjective.position - transform.position), _steeringSpeed * Time.deltaTime);
	}
}
