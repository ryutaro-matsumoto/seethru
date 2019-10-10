using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
	private float angleRad;

	public float moveSpeed = 10.0f;
	public int reflect = 2;

	Rigidbody2D rb2d;

	TimeDestroy td;

	public override void Init() {
		rb2d = GetComponent<Rigidbody2D>();
		angleRad = (transform.rotation.eulerAngles.z + 90f) * Mathf.Deg2Rad;

		Debug.Log("eulerAngle " + transform.rotation.eulerAngles.z);

		Vector2 moveVector = Vector2.zero;

		moveVector.x = moveSpeed * Mathf.Cos(angleRad);
		moveVector.y = moveSpeed * Mathf.Sin(angleRad);

		rb2d.AddForce(moveVector);

		td = GetComponent<TimeDestroy>();
		td.TimeStart();
	}

	private void FixedUpdate() {
		if(td.timeUp){
			ReturnToPool();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Gap"){
			HitEffect();
			return;
		}

		reflect--;
		if(reflect < 0){
			HitEffect();
		}
	}

	private void HitEffect(){
		ReturnToPool();
	}

}
