﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSencer : MonoBehaviour
{
	List<GameObject> reflecterList;

	[HideInInspector]
	public bool isReflect = false;

    // Start is called before the first frame update
    void OnEnable() {
		reflecterList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update(){
		GameObject reflecter = ReflecterJudgement();

		if(reflecter == null){
			isReflect = false;
			return;
		}

		
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Reflecter"){
			reflecterList.Add(collision.gameObject);
		}
	}

	private GameObject ReflecterJudgement(){
		Vector2 vec = Vector2.zero;
		GameObject nearlyObject = null;


		foreach(var obj in reflecterList) {
			if(vec == Vector2.zero){
				nearlyObject = obj;
				vec = obj.transform.position;
				continue;
			}

			Vector2 objPos = obj.transform.position;
			if(objPos.magnitude < vec.magnitude){
				nearlyObject = obj;
				vec = obj.transform.position;
				continue;
			}
		}


		return nearlyObject;
	}
}
