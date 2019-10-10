using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
	private float startTime;

	public float count;

	public bool timeUp = false;
    // Start is called before the first frame update
    void Start(){
		startTime = Time.time;
		timeUp = false;
	}

	// Update is called once per frame
	void Update()
    {
		float time = Time.time;
        if(time - startTime > count){
			timeUp = true;
			Debug.Log("消滅");
		}
		Debug.Log(time - startTime);
	}

	public void TimeStart(){
		startTime = Time.time;
		timeUp = false;
	}
}
