using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
	MMJGameServer.Timer timer = new MMJGameServer.Timer();
	public int StartCount = 3;

	private int nowCount = 0;
	private int prevCount = 0;

	private bool isCountDown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(isCountDown){
			nowCount = StartCount - timer.IElapsedSeconds;
			if (nowCount < prevCount) {
				CountDown(nowCount);
			}
			prevCount = nowCount;
		}
	}

	public void CountStart(){
		timer.Start();
		nowCount = StartCount;
		prevCount = StartCount;
		CountDown(StartCount);
		isCountDown = true;
	}

	private void CountDown(int cnt){


	}
}
