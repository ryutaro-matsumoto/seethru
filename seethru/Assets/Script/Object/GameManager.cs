using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	static public bool onNetwork = false;

	static private MrsClient connection;
	static public uint playerNum = 0;
	static public GameObject[] players;
	static public uint playID;
	static public GameObject bulletPool = null;

	static public string stageName = "Stage";

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	static public void ReceiveID(uint id){
		playID = id;
	}

	static public void GameStart(uint _stageId, uint[] _tabelIds, uint _playerNum){
		StageSceneTranslation(_stageId);
		PlayersInit(_tabelIds, _playerNum);
	}

	static public void ProtoStart(uint[] _tabelIds, uint _playerNum) {
		ProtoStageSceneTranslation();
		PlayersInit(_tabelIds, _playerNum);
	}

	static private void ProtoStageSceneTranslation(){
		SceneManager.LoadScene("ProtoScene");
	}

	static private void StageSceneTranslation(uint _stageId){
		SceneManager.LoadScene(stageName + _stageId);
	}

	static private void PlayersInit(uint[] _tableIds, uint _playerNum){
		if(_tableIds.Length > _playerNum) {
			Debug.LogError("Too many table IDs");
			return;
		}

		playerNum = _playerNum;

		GameObject[] startPositions = GameObject.FindGameObjectsWithTag("StartPosition");

		GameObject otherPlayerPrefab = (GameObject)Resources.Load("Object/OtherPlayer");
		GameObject mainPlayerPrefab = (GameObject)Resources.Load("Object/MainPlayer");
		
		for (int i = 0; i < playerNum; ++i){
			if(i == playID){
				players[i] = MonoBehaviour.Instantiate(mainPlayerPrefab, startPositions[_tableIds[i]].transform.position, startPositions[_tableIds[i]].transform.rotation);
				continue;
			}
			players[i] = MonoBehaviour.Instantiate(mainPlayerPrefab, startPositions[_tableIds[i]].transform.position, startPositions[_tableIds[i]].transform.rotation);
		}
	}
}