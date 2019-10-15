using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
	private float angle;

	Rigidbody2D rb2d;
	Timer td;

	public float moveSpeed = 10.0f;

	BulletGapHit gapHit;
	ReflectionObject reflection;

	public int id;


	public override void Init() {
		rb2d = GetComponent<Rigidbody2D>();
		angle = transform.rotation.eulerAngles.z + 90f;

		Vector2 moveVector = Vector2.zero;

		moveVector.x = moveSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
		moveVector.y = moveSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);

		rb2d.velocity = moveVector;

		td = GetComponent<Timer>();
		td.TimeStart();

		gapHit = transform.GetChild(1).GetComponent<BulletGapHit>();
		reflection = GetComponent<ReflectionObject>();

		id = -1;
	}

	private void Update() {
		angle = transform.eulerAngles.z - 90f;
		if(td.timeUp){
			ReturnToPool();
		}

		if(gapHit.GapHit||reflection.isDead){
			HitEffect();
		}
	}

	private void HitEffect(){
		ReturnToPool();
	}

}
