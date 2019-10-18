using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChip : MonoBehaviour
{
	public float fallSpeed = 1f;

	private bool isFall = false;

	private float scale;

	private void Start() {
		scale = transform.localScale.x;
	}

	private void Update() {
		if(isFall){
			scale -= fallSpeed * Time.deltaTime;
			transform.localScale = new Vector3(scale, scale, 1f);
			if (scale <= 0) {
				gameObject.SetActive(false);
			}
		}
	}

	public void Fall(){
		GetComponent<BoxCollider2D>().enabled = false;
		//StartCoroutine("FallCoroutine");
		isFall = true;
	}

	//public IEnumerator FallCoroutine(){
	//	float scale = transform.localScale.x;
	//	bool loop = true;
	//	while(loop){
	//		scale -= fallSpeed * Time.fixedTime;
	//		transform.localScale = new Vector3(scale, scale, 1f);
	//		if(scale <= 0){
	//			gameObject.SetActive(false);
	//		}
	//		yield return new WaitForSeconds(Time.fixedTime);
	//	}
	//}
}
