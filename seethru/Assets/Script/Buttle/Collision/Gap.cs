using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour {

	public bool Hit;
	Player player;


	private void Start() {
		Hit = false;
		player = transform.parent.GetComponent<Player>();
	}


	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Bullet"){
			Hit = true;
			player.SendDeadHit(collision.gameObject.GetComponent<Bullet>().id);
			Debug.Log("hit");
		}
	}

}
