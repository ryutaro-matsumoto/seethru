using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionObject : MonoBehaviour {
	Rigidbody2D rigidbody2d;
	BoxCollider2D boxCollider;
	[SerializeField]
	public int reflect;

	public bool isDead = false;

	public int startReflect = 2;

	// Start is called before the first frame update
	void OnEnable() {
		isDead = false;
		rigidbody2d = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		reflect = startReflect;
	}

	// Update is called once per frame
	void Update() {

	}


	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Reflecter") {
			reflect--;
			if (reflect < 0) {
				isDead = true;
				return;
			}
			Vector2 vec = rigidbody2d.velocity;

			Debug.Log(vec);


			ReflectCollider rc = collision.gameObject.GetComponent<ReflectCollider>();
			Vector2 ans = rc.ReflectVector(vec);
			rigidbody2d.velocity = ans * vec.magnitude;


			float angleRad = Mathf.Atan2(ans.y, ans.x);
			transform.eulerAngles = new Vector3(0f, 0f, angleRad * Mathf.Rad2Deg - 90f);

			rigidbody2d.angularVelocity = 0f;
		}
	}
}