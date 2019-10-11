﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectCollider : MonoBehaviour{
	[HideInInspector]
	public Vector2 rayVector;
	public bool isDebug = false;


    // Start is called before the first frame update
    void Start(){
		float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
		rayVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    // Update is called once per frame
    void Update(){
		float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
		rayVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
	}
}
