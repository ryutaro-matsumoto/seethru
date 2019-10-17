using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour{
	enum CollisionID{
		GAP = 3,
		GUARD
	}

	private void Start() {
		if(GameManager.onNetwork){
			transform.GetChild((int)CollisionID.GAP).gameObject.SetActive(false);
			transform.GetChild((int)CollisionID.GUARD).gameObject.SetActive(false);
		}
	}

}
