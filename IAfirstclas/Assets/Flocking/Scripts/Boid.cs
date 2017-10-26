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
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, _direction, _steeringSpeed * Time.deltaTime);
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 3, Color.green);
    }
}
