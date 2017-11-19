using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNShip : MonoBehaviour
{
    public int _maxActions;
    public float _score;
    public NNChromosome _genome;
    public GameObject _target;
    private float _hitVelocity;
    private float _flyingTime;

    public float _rotationPower;
    public float _thrusterPower;
    public float _timer;

    public float _distanceModifier;
    public float _flyingModifier;
    public float _hitModifier;
    public float _objectiveReward;
    private bool _isFlying = true;

    private Rigidbody _rgb;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public NeuronalNetwork _brain;

    public int inputs;
    public int outputs;
    public int neuronsPerLayer;
    public int hiddenLayers;
    public float linearGrade;
    public float bias;

    List<float> _inputs;
    List<float> _outputs;

    void Awake()
    {
        _brain = new NeuronalNetwork(inputs, outputs, hiddenLayers, neuronsPerLayer, linearGrade, bias);
        _brain.CreateNet();
        _genome = _brain.GetWeights();
    }

    void Start()
    {
        gameObject.name = "Ship#" + Random.Range(0, 100);
        _rgb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _inputs = new List<float>();
        _outputs = new List<float>();
    }

    public void UpdateWeights()
    {
        _brain.SetWeights(_genome);
    }

    void FixedUpdate()
    {
        if (_isFlying)
        {
            _flyingTime += Time.fixedDeltaTime;
        }
        _timer += Time.fixedDeltaTime;

        _inputs.Add((_target.transform.position - transform.position).normalized.x);
        _inputs.Add((_target.transform.position - transform.position).normalized.y);

        _inputs.Add(transform.up.normalized.x);
        _inputs.Add(transform.up.normalized.y);

        _outputs = _brain.UpdateNN(_inputs);

        if (_isFlying)
        {
         ApplyTruster(_outputs[0]);
         RotateLeft(_outputs[1]);
         RotateRight(_outputs[2]);
        }

        _inputs.Clear();
        _outputs.Clear();
    }

    public void RotateLeft(float output)
    {
        transform.Rotate(transform.forward * (_rotationPower * output) * Time.fixedDeltaTime);
    }

    public void RotateRight(float output)
    {
        transform.Rotate(-transform.forward * (_rotationPower * output) * Time.fixedDeltaTime);
    }

    public void ApplyTruster(float output)
    {
        _rgb.AddForce(transform.up * (_thrusterPower * output), ForceMode.Acceleration);
    }

    public void ResetPos()
    {
        _hitVelocity = 50f;
        _flyingTime = 0;
        _isFlying = true;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rgb.isKinematic = true;
        _rgb.isKinematic = false;
    }

    public void CheckScore()
    {
        if (_hitVelocity < 1)
        {
            _hitVelocity = 1;
        }
        float finalDistance = Vector3.Distance(transform.position, _target.transform.position);
        _score = _distanceModifier / finalDistance + _hitModifier / _hitVelocity + _flyingTime * _flyingModifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isFlying = false;
        if (_hitVelocity == 0)
        {
            _hitVelocity = collision.relativeVelocity.magnitude;
        }
        if (collision.gameObject.tag == "Objective" && _hitVelocity - _objectiveReward > 1)
        {
            _hitVelocity -= _objectiveReward;
        }

    }
}
