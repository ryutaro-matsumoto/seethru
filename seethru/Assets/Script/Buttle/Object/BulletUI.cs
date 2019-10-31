using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		Player player = GameManager.players[GameManager.playID].GetComponent<Player>();
		int max = player.startBullet;

		for (int i = 0; i < max; ++i) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = 0; i < player.bullet; ++i){
			transform.GetChild(i).gameObject.SetActive(true);
		}
    }
}
