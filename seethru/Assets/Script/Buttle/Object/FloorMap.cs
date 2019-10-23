using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMap : MonoBehaviour
{
	public int fallCount = 0;

	public float floorFallSeconds = 0.2f;


	public void FallFloor(){
		if(fallCount < transform.childCount){
			transform.GetChild(fallCount).GetComponent<FloorSet>().StartFallFloor(floorFallSeconds);
		}
	}


}
