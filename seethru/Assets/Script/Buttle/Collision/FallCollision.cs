using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour
{
	
	private void OnTriggerExit2D(Collider2D collision) {
		if(collision.gameObject.tag == "Floor"){
			transform.parent.GetComponent<FallDead>().SendFall();
		}
	}

}
