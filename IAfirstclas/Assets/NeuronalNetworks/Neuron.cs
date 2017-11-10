using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour {

    float _activation;
    float[] _weights;
	// Use this for initialization
	void Start () {
        _weights = new float[10];
        for (int i = 0; i < _weights.Length; i++)
        {
            _weights[i] = Random.Range(-1f, 1f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activation(List<Neuron> _inputs)
    {
        for (int i = 0; i < _inputs.Count; i++)
        {
  //         _activation += _inputs[i] * _weights[i];
  //         _activation += _bias * _weights[_inputs.Count];
        }
    }
}
