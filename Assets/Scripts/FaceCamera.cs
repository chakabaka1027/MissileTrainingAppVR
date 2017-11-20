﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Camera.main.transform != null){
            transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward/*, Camera.main.transform.rotation * Vector3.up*/);
	    }
    }
}
