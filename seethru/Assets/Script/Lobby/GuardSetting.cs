using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSetting : MonoBehaviour
{

	public int guardSetNum = 0;

	private void Update() {
		SetGuardData();
	}

	public void SetGuardData(){

		int cnt = 0;
		for(int i = 0; i < transform.childCount - 1; ++i){
			if(transform.GetChild(i).GetComponent<GuardSetButton>().isSelect){
				GameManager.guards[cnt] = (Guard.GuardColliderType)i;
				cnt++;
			}
		}
	}

}
