using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public enum GuardColliderType{
		GUARD_FRONT = 0,
		GUARD_LEFT,
		GUARD_BACK,
		GUARD_RIGHT
	};

	public GuardColliderType[] typeArray;


    // Start is called before the first frame update
    void Start(){
		SetGuard();
	}


	void SetGuard(){
		for(int i = 0; i < 4; i++){
			transform.GetChild(i).gameObject.SetActive(false);
			transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false; ;
		}

		for (int i = 0; i < typeArray.Length; i++){
			transform.GetChild((int)typeArray[i]).gameObject.SetActive(true);
			transform.GetChild((int)typeArray[i]).GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
