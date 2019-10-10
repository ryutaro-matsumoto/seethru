using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour {

	public bool Hit;

	private void Start() {
		Hit = false;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Bullet"){
			Hit = true;
			Debug.Log("Hit");
		}
	}

}
