using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public Vector3 _direction;
    public List<Boid> _neighbors;
    public float speed;

	// Use this for initialization
	void Start () {
        _neighbors = new List<Boid>();
	}
	
	// Update is called once per frame
	void Update () {        
        transform.position = Vector3.MoveTowards(transform.position, _direction, speed * Time.deltaTime);
	}
}
