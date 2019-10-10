using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : MonoBehaviour
{
	bool isTriggerStay = false;

	public bool triggerStay { get { return isTriggerStay; } }

	public string hitTag;

	private void OnTriggerStay2D(Collider2D collision) {
		if(collision.tag == hitTag){
			isTriggerStay = true;
		}
	}


	private void OnTriggerExit2D(Collider2D collision) {
		if(collision.tag == hitTag){
			isTriggerStay = false;
		}
	}
}
