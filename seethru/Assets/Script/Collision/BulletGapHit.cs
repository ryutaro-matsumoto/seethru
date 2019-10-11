using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGapHit : MonoBehaviour
{
	[HideInInspector]
	public bool GapHit = false;

	private void OnEnable() {
		GapHit = false;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Gap") {
			GapHit = true;
			return;
		}
	}

}
