using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiLine : MonoBehaviour {

    public Transform uiPos;
    public Transform objPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<LineRenderer>().SetPosition(0, uiPos.position);
		GetComponent<LineRenderer>().SetPosition(1, objPos.position);
	}
}
