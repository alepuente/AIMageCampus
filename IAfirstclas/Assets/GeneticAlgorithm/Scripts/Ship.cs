using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {   

    public float _score;
    public Chromosome _genome;
    public GameObject _target;
    
    private Rigidbody _rgb;
    public float _rotationPower;
    public float _thrusterPower;
    public float _timer;

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
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rgb.isKinematic = true;
        _rgb.isKinematic = false;
    }

    public void CheckScore()
    {
        _score = 1000 / Vector3.Distance(transform.position, _target.transform.position);
    }
}
