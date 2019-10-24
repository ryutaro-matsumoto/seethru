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
	[HideInInspector]
	public bool isHit = false;

	[HideInInspector]
	public Animator anim;

	private void Start() {
		bullet = startBullet;
		if(GetComponent<PlayerInput>() != null){
			anim = transform.GetChild(6).GetComponent<Animator>();
		}
		else{
			anim = transform.GetChild(4).GetComponent<Animator>();
		}
	}

	// Update is called once per frame
	void Update() {
        if(isHit){
			SendDeadHit();
		}
		if(isDead){
			DeadPlayer();
		}
    }

	private void SendDeadHit(){
		if(GameManager.onNetwork){
			/*MrsClient待ち*/

		}
		else {
			DeadPlayer();
		}
	}

	public void DeadPlayer(){
		GameManager.livePlayer--;
		gameObject.SetActive(false);
	}
}
