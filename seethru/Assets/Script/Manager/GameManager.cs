using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

	static public bool onNetwork{ get{ return connection != null; } }

	static public MrsClient connection;
	static public uint playerNum = 0;
	static public GameObject[] players;
	static public uint playID;
	static public Pool bulletPool = null;

	static public string stageName = "Stage";

	static private int[] startPositonIDTable;	

	static private Color[] playerColor = new Color[4];

	static public uint livePlayer = 0;

	static public FloorMap floorMap;
	static public GameObject timeControler;

 	private void Awake() {
		DontDestroyOnLoad(this);
	}


	//------------------------------------------------------------------------
	/// <summary>
	/// 接続成功時、この関数を呼び出してね(MrsClient側で使用)
	/// </summary>
	/// <param name="id">サーバーから送られてきたID</param>
	/// <param name="client">接続成功したクライアントクラス</param>
	static public void ConnectionServer(uint id, MrsClient client){
		ReceiveID(id);
		SetConnection(client);
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// ID設定
	/// </summary>
	/// <param name="id">設定するID</param>
	static public void ReceiveID(uint id){
		playID = id;
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// 接続成功時、この関数を呼び出してコネクションを保持させてね
	/// </summary>
	/// <param name="client">接続したクライアントクラス</param>
	public static void SetConnection(MrsClient client) {
		connection = client;
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// サーバーからの指示を受けたときに実行。これでステージシーンに移動する。(MrsClient側で使用)
	/// </summary>
	/// <param name="_stageId">ステージのID</param>
	/// <param name="_tabelIds">初期位置のID</param>
	/// <param name="_playerNum">playerの数</param>
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
		FadeManeger.Fadeout("ProtoScene");
		//SceneManager.LoadScene("ProtoScene");
	}

	static private void StageSceneTranslation(uint _stageId){
		SceneManager.LoadScene(stageName + _stageId);
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// ステージシーンに移った直後に実行(ゲーム側で使用)
	/// </summary>
	static public void PlayersInit() {
		if (4 < playerNum) {
			Debug.LogError("Too many players");
			return;
		}

		livePlayer = playerNum;

		playerColor[0] = new Color(1f, 1f, 1f);
		playerColor[1] = new Color(0f, 1f, 0f);
		playerColor[2] = new Color(0f, 0f, 1f);
		playerColor[3] = new Color(1f, 0f, 0f);

		players = new GameObject[playerNum];

		GameObject[] startPositions = GameObject.FindGameObjectsWithTag("StartPosition");

		GameObject otherPlayerPrefab = (GameObject)Resources.Load("Prefab/Object/OtherPlayer");
		GameObject mainPlayerPrefab = (GameObject)Resources.Load("Prefab/Object/MainPlayer");

		Debug.Log(otherPlayerPrefab);

		Debug.Log(startPositions.Length);

		for (int i = 0; i < playerNum; ++i) {
			if (i == playID) {
				players[i] = MonoBehaviour.Instantiate(mainPlayerPrefab, startPositions[startPositonIDTable[i]].transform.position, startPositions[startPositonIDTable[i]].transform.rotation);
				players[i].transform.GetChild(6).GetChild(2).GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
				players[i].transform.GetChild(6).GetChild(2).GetChild(3).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
				players[i].transform.GetChild(6).GetChild(2).GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
				continue;
			}
			players[i] = MonoBehaviour.Instantiate(otherPlayerPrefab, startPositions[startPositonIDTable[i]].transform.position, startPositions[startPositonIDTable[i]].transform.rotation);
			players[i].transform.GetChild(4).GetChild(2).GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
			players[i].transform.GetChild(4).GetChild(2).GetChild(3).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
			players[i].transform.GetChild(4).GetChild(2).GetChild(5).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColor[i];
			if(GameManager.onNetwork){
				players[i].transform.GetChild(2).gameObject.SetActive(false);
				players[i].transform.GetChild(3).gameObject.SetActive(false);
			}
		}

		bulletPool = GameObject.Find("BulletPool").GetComponent<Pool>();

		if(onNetwork){
			connection.SendStartingPos();
		}
	}


	//------------------------------------------------------------------------
	/// <summary>
	/// 接続終了時呼び出し(MrsClient側で使用)
	/// </summary>
	public static void OffConnection(){
		connection = null;
	}

	public static void FallFloor(){
		floorMap.FallFloor();
	}

	public static void CountDown(int cnt){
		timeControler.GetComponent<CountDownTimer>().CountDown(cnt);
	}

	public static void CountStart(){
		timeControler.GetComponent<CountDownTimer>().CountStart();
	}
}