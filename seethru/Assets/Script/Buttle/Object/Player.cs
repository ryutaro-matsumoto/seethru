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

	public Vector2 receivePos;

	Rigidbody2D rb2d;

	private void Start() {
		bullet = startBullet;
		if(GetComponent<PlayerInput>() != null){
			anim = transform.GetChild(6).GetComponent<Animator>();
		}
		else{
			anim = transform.GetChild(4).GetComponent<Animator>();
		}
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update() {
		if(isDead){
			DeadPlayer();
		}

		if(GetComponent<PlayerInput>() == null){
			Vector2 velo = (receivePos - (Vector2)transform.position);
			rb2d.velocity = velo;
		}
    }

	public void SendDeadHit(int bulletID){
		if(GameManager.onNetwork){
			/*MrsClient待ち*/
			GameManager.connection.SendPlayerDeadHit(bulletID);
		}
		else {
			DeadPlayer();
		}
	}

	public void DeadPlayer(){
		gameObject.SetActive(false);
	}
}
