using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouSpirte : MonoBehaviour
{
	Timer tm;

	private void Start() {
		tm = GetComponent<Timer>();
	}
	// Update is called once per frame
	void Update()
    {
		transform.localEulerAngles = -transform.parent.eulerAngles;
		if(tm.timeUp){
			gameObject.SetActive(false);
		}
	}
}
