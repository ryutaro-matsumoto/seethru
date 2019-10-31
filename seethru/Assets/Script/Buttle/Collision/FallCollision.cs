using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour
{
	
	private void OnTriggerExit2D(Collider2D collision) {
		if(collision.gameObject.tag == "Floor"){
			if(!transform.parent.GetComponent<Player>().isDead){

				transform.parent.GetComponent<FallDead>().SendFall();
				gameObject.SetActive(false);
			}
		}
	}
}
