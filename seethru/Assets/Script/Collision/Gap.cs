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


	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Bullet"){
			Hit = true;
			player.isDead = true;
			Debug.Log("Hit");
		}
	}

}
