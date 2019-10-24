using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadZone : MonoBehaviour
{
	public int coolTime = 0;
	public int reloadNum = 1;

	BoxCollider2D coll;

	private void Start() {
		coll = GetComponent<BoxCollider2D>();
	}

	IEnumerator CoolTimeCoroutine(){
		int coolTimeCount = coolTime;
		while(coolTimeCount > 0){

			coolTimeCount--;
			yield return new WaitForSeconds(1f);
		}

		coll.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Reload"){
			Player player = collision.transform.parent.GetComponent<Player>();
			if (player.bullet < player.startBullet) {
				player.bullet += reloadNum;
				if(player.bullet > player.startBullet){
					player.bullet = player.startBullet;
				}
			}

			coll.enabled = false;
			StartCoroutine("CoolTimeCoroutine");
		}
	}
}