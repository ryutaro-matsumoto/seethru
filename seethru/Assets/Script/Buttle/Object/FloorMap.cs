using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMap : MonoBehaviour
{
	public int fallCount = 1;

	public float floorFallSeconds = 0.2f;

	private void Awake() {
		GameManager.floorMap = this;
	}

	public void FallFloor(){
		if(fallCount < transform.childCount){
			transform.GetChild(fallCount).GetComponent<FloorSet>().StartFallFloor(floorFallSeconds);
			fallCount++;
		}
	}
}
