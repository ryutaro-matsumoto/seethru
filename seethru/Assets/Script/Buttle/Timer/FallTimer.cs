using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTimer : MonoBehaviour
{
	public float fallInterval = 20f;

	private FloorMap floormap;
	private readonly MMJGameServer.Timer timer = new MMJGameServer.Timer();

	bool onTimer = false;
	// Start is called before the first frame update
	void Start()
    {
		floormap = GameManager.floorMap;
    }

    // Update is called once per frame
    void Update(){
		if(onTimer){
			if (timer.FElapsedSeconds > fallInterval) {
				timer.Stop();
				timer.Reset();
				timer.Start();
				Fall();
			}
		}
	}

	public void StartTimer(){
		if(GameManager.playID == 0){
			timer.Start();
			onTimer = true;
		}
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
