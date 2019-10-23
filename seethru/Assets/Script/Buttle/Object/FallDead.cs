using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDead : MonoBehaviour
{

	public float fallSpeed = 1f;
	public float scaleSpeed = 2f;

	private bool isFall = false;

	private float scale;

	// Start is called before the first frame update
	void Start()
    {
		scale = transform.localScale.x;
	}
	private void Update() {
		if (isFall) {
			scale -= fallSpeed * Time.deltaTime;
			transform.localScale = new Vector3(scale, scale, scale);
			if (scale <= 0) {
				GetComponent<Player>().isDead = true;
			}

			Vector3 newPosition = transform.position;
			newPosition.z += scaleSpeed * Time.deltaTime; ;

			transform.position = newPosition;
		}
	}

	public void Fall() {
		isFall = true;
		PlayerInput input = GetComponent<PlayerInput>();
		if(input != null){
			input.isInput = false;
		}
	}
}
