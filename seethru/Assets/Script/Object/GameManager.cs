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

	static private int[] startPositonIDTable;	

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	static public void ReceiveID(uint id){
		playID = id;
	}

	static public void GameStart(uint _stageId, int[] _tabelIds, uint _playerNum){
		StageSceneTranslation(_stageId);
		playerNum = _playerNum;
		startPositonIDTable = _tabelIds;
	}

	static public void ProtoStart(int[] _tabelIds, uint _playerNum) {
		ProtoStageSceneTranslation();

		playerNum = _playerNum;
		startPositonIDTable = _tabelIds;
		//PlayersInit(_tabelIds, _playerNum);
	}

	static private void ProtoStageSceneTranslation(){
		SceneManager.LoadScene("ProtoScene");
	}

	static private void StageSceneTranslation(uint _stageId){
		SceneManager.LoadScene(stageName + _stageId);
	}

	static public void PlayersInit(){
		if(startPositonIDTable.Length > playerNum) {
			Debug.LogError("Too many table IDs");
			return;
		}

		players = new GameObject[playerNum];


		GameObject[] startPositions = GameObject.FindGameObjectsWithTag("StartPosition");

		GameObject otherPlayerPrefab = (GameObject)Resources.Load("Prefab/Object/OtherPlayer");
		GameObject mainPlayerPrefab = (GameObject)Resources.Load("Prefab/Object/MainPlayer");

		Debug.Log(otherPlayerPrefab);

		Debug.Log(startPositions.Length);

		for (int i = 0; i < playerNum; ++i){
			if(i == playID){
				players[i] = MonoBehaviour.Instantiate(mainPlayerPrefab, startPositions[startPositonIDTable[i]].transform.position, startPositions[startPositonIDTable[i]].transform.rotation);
				continue;
			}
			players[i] = MonoBehaviour.Instantiate(otherPlayerPrefab, startPositions[startPositonIDTable[i]].transform.position, startPositions[startPositonIDTable[i]].transform.rotation);
		}
	}
}