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

	public float animationRange;


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
		if(GameManager.onNetwork){
			if (GetComponent<PlayerInput>() == null) {
				Vector2 vec = Vector2.Lerp(transform.position, receivePos, 0.5f);
				Vector2 pos = transform.position;
				transform.position = new Vector3(vec.x, vec.y, transform.position.z);
				
				vec -= pos;
				if (vec.magnitude > animationRange) {
					anim.SetBool("Run", true);
					anim.SetFloat("RunSpeed", 1f);
				}
				else {
					anim.SetBool("Run", false); ;
				}
			}
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
		GameObject effect = (GameObject)Resources.Load(GameManager.effectPath + "Water/Splash");
		Vector3 newPosition = transform.position;
		newPosition.z = transform.position.z - 2f;

		Instantiate(effect, newPosition, transform.rotation);

		GameManager.soundManager.PlaySeInit((int)SoundManager.SEIndex.Hit);

		isDead = true;
	}
}
