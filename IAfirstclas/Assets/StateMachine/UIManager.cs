using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    public Text _goldAm;
    public Text _minerAm;

    public static UIManager _instance;

	// Use this for initialization
	void Start () {
        _instance = this;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
