using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public enum GuardColliderType{
		GAP = 0,
		GUARD_FRONT,
		GUARD_LEFT,
		GUARD_BACK,
		GUARD_RIGHT
	};

	public GuardColliderType[] typeArray;

    // Start is called before the first frame update
    void Start(){
		SetGuard();
    }

    // Update is called once per frame
    void Update(){
        
    }

	void SetGuard(){
		for(int i = 1; i < 5; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}

		for (int i = 0; i < typeArray.Length; i++){
			transform.GetChild((int)typeArray[i]).gameObject.SetActive(true);
		}
	}
}
