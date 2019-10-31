using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDead : MonoBehaviour
{

	public float fallSpeed = 1f;
	public float scaleSpeed = 2f;

	private bool isFall = false;

	private float scale;

	private float speed = 0;

	// Start is called before the first frame update
	void Start()
    {
		scale = transform.localScale.x;
	}
	private void Update() {
		if (isFall) {
			scale -= fallSpeed * Time.deltaTime;
			//transform.localScale = new Vector3(scale, scale, scale);
			if (scale <= 0) {
				GetComponent<Player>().isDead = true;
			}

			Vector3 newPosition = transform.position;
			speed += scaleSpeed * Time.deltaTime;

			newPosition.z += speed;
			transform.position = newPosition;
		}
	}

	public void SendFall(){
		if(GameManager.onNetwork){
			/* MrsClientの関数待ち */
			GameManager.connection.SendPlayerDeadFall();
		}
		else{
			Fall();
		}

	}
	public void Fall() {
		isFall = true;
		GameManager.soundManager.PlaySeInit((int)SoundManager.SEIndex.Fall_High);
		//	PlayerInput input = GetComponent<PlayerInput>();
		//	if(input != null){
		//		input.isInput = false;
		//	}
	}
}
