using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTimer : MonoBehaviour
{
	public float fallInterval = 20f;

	private FloorMap floormap;
	private MMJGameServer.Timer timer = new MMJGameServer.Timer();

	private int fallCount = 1;

	// Start is called before the first frame update
	void Start()
    {
		floormap = GameManager.floorMap;
		StartTimer();
    }

    // Update is called once per frame
    void Update(){
		if (timer.FElapsedSeconds > fallInterval * (float)fallCount) {
			fallCount++;
			Fall();
		}
    }

	public void StartTimer(){
		timer.Start();
	}

	private void Fall(){
		if(GameManager.onNetwork){
			GameManager.connection.SendFallFloor();
		}
		else{
			floormap.FallFloor();
		}
	}
}
