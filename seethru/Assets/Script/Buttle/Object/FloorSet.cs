using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSet : MonoBehaviour
{
	public void StartFallFloor(float floorFallSeconds) {
		StartCoroutine("FallFloor", floorFallSeconds);
	}

	IEnumerator FallFloor(float floorFallSeconds) {
		for(int i = 0; i < transform.childCount; ++i){ 
			transform.GetChild(i).GetComponent<FloorChip>().FallNotice();
			GameManager.soundManager.PlaySeInit((int)SoundManager.SEIndex.Floor1);
			yield return new WaitForSeconds(floorFallSeconds);
		}

		yield return new WaitForSeconds(1.0f);

		for (int i = 0; i < transform.childCount; ++i) {
			transform.GetChild(i).GetComponent<FloorChip>().Fall();
		}
		GameManager.soundManager.PlaySeInit((int)SoundManager.SEIndex.Fall_Low);

	}
}
