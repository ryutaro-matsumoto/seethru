using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
	MMJGameServer.Timer timer = new MMJGameServer.Timer();
	public int startCount = 0;

	public float interval = 3f;

	private bool isCountDown = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.playID == 0)
        {
            SendCountStart();
        }
    }

	private void Awake() {
		GameManager.timeControler = gameObject;
	}

	// Update is called once per frame
	void Update()
    {
		if(isCountDown && GameManager.playID == 0){
			if (timer.FElapsedSeconds > interval) {
				timer.Stop();
				timer.Reset();
				timer.Start();
				SendCountDown(startCount);
				startCount++;
			}
		}
	}

	public void CountStart(){
		timer.Start();
		transform.GetChild(0).gameObject.SetActive(true);
		startCount++;
		isCountDown = true;
	}

	public void CountDown(int cnt){
		transform.GetChild(cnt - 1).gameObject.SetActive(false);
		transform.GetChild(cnt).gameObject.SetActive(true);

		if(cnt >= transform.childCount - 1){
			GetComponent<FallTimer>().StartTimer();
			GameManager.players[GameManager.playID].GetComponent<PlayerInput>().isInput = true;
 			isCountDown = false; 
		}
	}

	private void SendCountDown(int cnt){
		if(GameManager.onNetwork)
        {
            GameManager.connection.SendCountDown(cnt);
        }
		else{
			CountDown(cnt);
		}
	}

	private void SendCountStart(){
		if (GameManager.onNetwork)
        {
            GameManager.connection.SendCountDownStart();
        }
		else {
			CountStart();
		}
	}
}
