using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionObject : MonoBehaviour {
	Rigidbody2D rigidbody2d;
	[SerializeField]
	public int reflect;

	public bool isDead = false;
	public int startReflect = 2;

	public Vector2 vector;
	// Start is called before the first frame update
	void OnEnable() {
		isDead = false;
		rigidbody2d = GetComponent<Rigidbody2D>();
		reflect = startReflect;
	}

	// Update is called once per frame
	void Update() {
		vector = rigidbody2d.velocity;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Reflecter"){
			reflect--;
			if (reflect < 0) {
				isDead = true;
				return;
			}

			foreach (ContactPoint2D contact in collision.contacts) {
				if(contact.otherCollider.gameObject == gameObject){
					Vector2 vec = vector;

					Vector2 ans = vec + 2 * Vector2.Dot(-vec, contact.normal) * contact.normal;

					rigidbody2d.velocity = ans.normalized * vec.magnitude;


					float angleRad = Mathf.Atan2(ans.y, ans.x);
					transform.eulerAngles = new Vector3(0f, 0f, angleRad * Mathf.Rad2Deg - 90f);

					rigidbody2d.angularVelocity = 0f;

					transform.position = contact.point;

					vector = ans.normalized * vec.magnitude;
				}
			}
		}
	}


	//private void OnTriggerEnter2D(Collider2D collision) {
	//	if (collision.gameObject.tag == "Reflecter") {
	//		reflect--;
	//		if (reflect < 0) {
	//			isDead = true;
	//			return;
	//		}
	//		Vector2 vec = rigidbody2d.velocity;

	//		Debug.Log(vec);


	//		ReflectCollider rc = collision.gameObject.GetComponent<ReflectCollider>();
	//		Vector2 ans = rc.ReflectVector(vec);
	//		rigidbody2d.velocity = ans * vec.magnitude;


	//		float angleRad = Mathf.Atan2(ans.y, ans.x);
	//		transform.eulerAngles = new Vector3(0f, 0f, angleRad * Mathf.Rad2Deg - 90f);

	//		rigidbody2d.angularVelocity = 0f;
	//	}
	//}
}