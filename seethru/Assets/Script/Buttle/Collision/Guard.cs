using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public enum GuardColliderType{
		GUARD_FRONT = 0,
		GUARD_BACK,
		GUARD_RIGHT,
		GUARD_LEFT
	};


    // Start is called before the first frame update
    void Start(){

	}

	public void Update() {
		transform.localEulerAngles = -transform.parent.eulerAngles;
	}


	public void SetGuard(GuardColliderType[] typeArray) {
		for(int i = 0; i < 4; i++){
			transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
		}

		for (int i = 0; i < typeArray.Length; i++){
			transform.GetChild(0).GetChild((int)typeArray[i]).gameObject.SetActive(true);
			transform.GetChild(0).GetChild((int)typeArray[i]).GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
