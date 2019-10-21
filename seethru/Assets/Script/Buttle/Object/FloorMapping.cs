using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMapping : MonoBehaviour
{
	public int width;
	public int height;

	public int fallCount = 0;
	private int startIndex = 0;

	public float floorFallSeconds = 0.2f;

    // Start is called before the first frame update
    void Awake(){
		GameObject floorPrefab = Resources.Load("Prefab/Object/FloorChip") as  GameObject;
		
		for(int i = 0; i < width / 2 && i < height / 2; i++ ){
			for(int j = i; j < width - i; ++j){
				Instantiate(floorPrefab, new Vector3((float)j - (float)width / 2f + 0.5f, (float)i - (float)height / 2f + 0.5f, 0f), Quaternion.identity).transform.SetParent(transform);
			}
			for (int j = i + 1; j < height - i; ++j) {
				Instantiate(floorPrefab, new Vector3((float)(width - 1 - i) - width / 2f + 0.5f, (float)j - (float)height / 2f + 0.5f, 0f), Quaternion.identity).transform.SetParent(transform);
			}
			for (int j = width - i - 2; j >= i; --j) {
				Instantiate(floorPrefab, new Vector3((float)j - (float)width / 2f + 0.5f, (float)(height - i - 1) - (float)height / 2f + 0.5f, 0f), Quaternion.identity).transform.SetParent(transform);
			}
			for (int j = height - i - 2; j >= i + 1; --j) {
				Instantiate(floorPrefab, new Vector3((float)i - width / 2f + 0.5f, (float)j - (float)height / 2f + 0.5f, 0f), Quaternion.identity).transform.SetParent(transform);
			}
		}
	}

	public void FallFloor(){
		StartCoroutine("FallFloorCoroutine");
	}

	IEnumerator FallFloorCoroutine(){
		int a = ((width - fallCount * 2) * 2 + (height - 2 - fallCount * 2) * 2);
		Debug.Log(a);
		for (int i = 0; i < a; ++i) {
			transform.GetChild(i + startIndex).GetComponent<FloorChip>().Fall();			
			yield return new WaitForSeconds(floorFallSeconds);
		}
		++fallCount;

	}
}
