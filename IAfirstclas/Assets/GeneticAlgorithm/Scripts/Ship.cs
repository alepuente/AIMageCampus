using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

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
    private bool _isFlying;

    private Rigidbody _rgb;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Awake()
    {
        _genome = new Chromosome();
    }

    void Start()
    {
        _rgb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Update()
    {
        if (_isFlying)
        {
            _flyingTime += Time.deltaTime;
        }
        _timer += Time.deltaTime;
        for (int i = 0; i < _genome._chromosome.Count; i++)
        {
            if (_timer < _genome._chromosome[i]._time)
            {
                switch (_genome._chromosome[i]._action)
                {
                    case Gen.Actions.ApplyTruster:
                        ApplyTruster();
                        break;
                    case Gen.Actions.RotateLeft:
                        RotateLeft();
                        break;
                    case Gen.Actions.RotateRight:
                        RotateRight();
                        break;
                    case Gen.Actions.DoNothing:
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
        transform.Rotate(transform.forward * _rotationPower * Time.deltaTime);
    }

    public void RotateRight()
    {
        transform.Rotate(-transform.forward * _rotationPower * Time.deltaTime);
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
        _hitVelocity = 0;
        _flyingTime = 0;
        _isFlying = true;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rgb.isKinematic = true;
        _rgb.isKinematic = false;
    }

    public void CheckScore()
    {
        if (_hitVelocity != 0)
        {
            _hitVelocity = _hitModifier / _hitVelocity;
        }
        _score = _distanceModifier / Vector3.Distance(transform.position, _target.transform.position) + _hitVelocity +  _flyingTime * _flyingModifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isFlying = false;
        _hitVelocity = collision.relativeVelocity.magnitude;
        Debug.Log("HIT");
    }
}
