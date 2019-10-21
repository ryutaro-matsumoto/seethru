using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	/// <summary>
	/// プレイヤーのオイラー角
	/// </summary>
	public float angle;
	public int startBullet = 2;
	public int bullet;
	public bool attackStop = false;

	[SerializeField]
	private float coolTime = 1.0f;

	[HideInInspector]
	public bool isDead = false;


	private void Start() {
		bullet = startBullet;
	}

	// Update is called once per frame
	void FixedUpdate() {
        if(isDead){
			DeadPlayer();
		}
    }

	void DeadPlayer(){
		GameManager.livePlayer--;
		gameObject.SetActive(false);
	}
}
