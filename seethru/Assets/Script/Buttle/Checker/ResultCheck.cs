using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultCheck : MonoBehaviour
{
	public string resultScene;

	private bool onResult = false;
    void Update()
    {
		uint liveCnt = 0;
		for(int i = 0; i < GameManager.playerNum; ++i){
			if(!GameManager.players[i].GetComponent<Player>().isDead){
				liveCnt++;
			}
		}
		GameManager.livePlayer = liveCnt;

		if (GameManager.livePlayer <= 1 && !onResult){
			SceneManager.LoadScene(resultScene, LoadSceneMode.Additive);
			onResult = true;
			if(GameManager.playID == 0){
				GameManager.connection.SendOnResult();
			}
			if(!GameManager.players[GameManager.playID].GetComponent<Player>().isDead){
				GameManager.players[GameManager.playID].GetComponent<PlayerInput>().isInput = false;				
			}
		}   
    }
}
