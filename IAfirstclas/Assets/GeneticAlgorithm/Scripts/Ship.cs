using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int _maxActions;
    public float _score;
    public Chromosome _genome;
    public GameObject _target;
    private float _hitVelocity;
    private float _flyingTime;
    public float _flyingModifier;

    public float _rotationPower;
    public float _thrusterPower;
    public float _timer;

    public float _distanceModifier;
    public float _hitModifier;
    public float _objectiveReward;
    private bool _isFlying = true;

    private Rigidbody _rgb;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Awake()
    {
        _genome = new Chromosome(_maxActions);
    }

    void Start()
    {
        gameObject.name = "Ship#" + Random.Range(0, 100);
        _rgb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (_isFlying)
        {
            _flyingTime += Time.fixedDeltaTime;
        }
        _timer += Time.fixedDeltaTime;
        for (int i = 0; i < _genome._chromosome.Count; i++)
        {
            if (_timer < _genome._chromosome[i]._time)
            {
                switch (_genome._chromosome[i]._action)
                {
                    case Chromosome.Actions.ApplyTruster:
                        ApplyTruster();
                        break;
                    case Chromosome.Actions.RotateLeft:
                        RotateLeft();
                        break;
                    case Chromosome.Actions.RotateRight:
                        RotateRight();
                        break;
                    case Chromosome.Actions.DoNothing:
                        DoNothing();
                        break;
                    default:
                        break;
                }
            }
        }
        /*if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        if (Input.GetKey(KeyCode.W))
        {
            ApplyTruster();
        }*/
    }

    public void RotateLeft()
    {
        transform.Rotate(transform.forward * _rotationPower * Time.fixedDeltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(-transform.forward * _rotationPower * Time.fixedDeltaTime);
    }

    public void ApplyTruster()
    {
        _rgb.AddForce(transform.up * _thrusterPower, ForceMode.Acceleration);
    }

    public void DoNothing()
    {
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
        if (collision.gameObject.tag == "Objective" && _hitVelocity -_objectiveReward > 1)
        {
            _hitVelocity -= _objectiveReward;
        }
        
    }
}
