using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
	private float angleRad;
	[HideInInspector]
	public int reflect;

	Rigidbody2D rb2d;
	Timer td;



	public float moveSpeed = 10.0f;
	[SerializeField]
	private int startReflect = 2;


	public override void Init() {
		rb2d = GetComponent<Rigidbody2D>();
		angleRad = (transform.rotation.eulerAngles.z + 90f) * Mathf.Deg2Rad;

		Debug.Log("eulerAngle " + transform.rotation.eulerAngles.z);

		Vector2 moveVector = Vector2.zero;

		moveVector.x = moveSpeed * Mathf.Cos(angleRad);
		moveVector.y = moveSpeed * Mathf.Sin(angleRad);

		rb2d.velocity = moveVector;

		td = GetComponent<Timer>();
		td.TimeStart();

		reflect = startReflect;
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
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("a");

		if (collision.gameObject.tag == "Reflecter"){
			reflect--;
			if (reflect < 0) {
				HitEffect();
				return;
			}

			Vector2 vec = rb2d.velocity; ;

			rb2d.velocity = Vector2.zero;
			rb2d.angularVelocity = 0.0f;

			ReflectCollider rc = collision.gameObject.GetComponent<ReflectCollider>();
			Debug.Log(rc.rayVector);

			Vector2 ans = vec - 2 * Vector2.Dot(vec, rc.rayVector) * rc.rayVector;

			ans.Normalize();

			rb2d.velocity = ans * moveSpeed;

			angleRad = Mathf.Atan2(ans.y, ans.x);

			transform.eulerAngles = new Vector3(0f, 0f, angleRad * Mathf.Rad2Deg - 90f);
		}

	}

	private void HitEffect(){
		ReturnToPool();
	}

}
