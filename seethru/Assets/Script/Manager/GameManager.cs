﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

	static public bool onNetwork{ get{ return connection != null; } }

	// プロフィール関係
	static public uint playID;
	static public string ipAddress = "192.168.252.120";
	static public string playerName = "Anonymous";

	static public DataStructures.S_DataProfile[] profiles = new DataStructures.S_DataProfile[4];

	static public string[] playerNames;


	static public MrsClient connection;
	static public uint playerNum = 0;
	static public GameObject[] players;

	static public readonly string stageName = "Stage";

	static private Color[] playerColor = new Color[4];

	static public uint livePlayer = 0;

	//ゲーム中のオブジェクト（毎回初期化）
	static private int[] startPositonIDTable;
	static public FloorMap floorMap;
	static public GameObject timeControler;
	static public StageSelect stageSelect;
	static public List<Bullet> bullets = new List<Bullet>();
	static public Pool bulletPool = null;

	private void Awake() {
		DontDestroyOnLoad(this);
	}


	//------------------------------------------------------------------------
	/// <summary>
	/// 接続成功時、この関数を呼び出してね(MrsClient側で使用)
	/// </summary>
	/// <param name="id">サーバーから送られてきたID</param>
	/// data<param name="client">接続成功したクライアントクラス</param>
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
		FadeManeger.Fadeout(stageName + _stageId);
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

	//------------------------------------------------------------------------
	/// <summary>
	/// 床を落とす指示を受信時に実行
	/// </summary>
	public static void FallFloor(){
		floorMap.FallFloor();
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// 開始時のカウントダウン同期
	/// </summary>
	public static void CountDown(int cnt){
		timeControler.GetComponent<CountDownTimer>().CountDown(cnt);
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// カウントダウンの開始の同期
	/// </summary>
	public static void CountStart(){
		timeControler.GetComponent<CountDownTimer>().CountStart();
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// ホストが選択したステージの同期
	/// </summary>
	/// <param name="stage">ステージの番号</param>
	public static void StageNumSelect(int stage){
		stageSelect.transform.GetChild(0).GetChild(stageSelect.selectStage - 1).gameObject.SetActive(false);
		stageSelect.selectStage = stage;
		stageSelect.transform.GetChild(0).GetChild(stageSelect.selectStage - 1).gameObject.SetActive(true);
	}


	//------------------------------------------------------------------------
	/// <summary>
	/// 弾の発射を受信時に使用
	/// </summary>
	/// <param name="vec"></param>
	/// <param name="qt"></param>
	/// <param name="playerID"></param>
	/// <param name="bulletID"></param>
	public static void BulletPlace(Vector2 vec, Quaternion qt, uint playerID, int bulletID){
		
		Bullet bullet = bulletPool.Place<Bullet>(vec, qt);
		bullet.id = bulletID;
		for(int i = 0; i < bullets.Count; ++i){
			if(bullets[i] != null){
				bullets[i] = bullet;
				return;
			}
		}

		bullets.Add(bullet);
		//players[playerID].GetComponent<Player>().anim.SetBool("Attack", true);
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// プレイヤーの落下判定受信時に使用
	/// </summary>
	/// <param name="playerID"></param>
	public static void PlayerDeadFall(int playerID){
		players[playerID].GetComponent<FallDead>().Fall();
	}

	//------------------------------------------------------------------------
	/// <summary>
	/// 弾に当たった判定を受信時に使用
	/// </summary>
	/// <param name="playerID"></param>
	/// <param name="bulletID"></param>
	public static void PlayerDeadHit(int playerID, int bulletID) {
		players[playerID].GetComponent<Player>().isDead = true;
		for (int i = 0; i < bullets.Count; ++i) {
			if (bullets[i].id == bulletID) {
				bullets[i].HitEffect();
			}
		}
	}
	//------------------------------------------------------------------------
	/// <summary>
	/// 弾の反射を受信時に使用
	/// </summary>
	/// <param name="bulletID"></param>
	public static void BulletReflection(int bulletID){

	}

	public static void BackRoom(){
		floorMap = null;
		timeControler = null;
		stageSelect = null;
		bullets = new List<Bullet>();
		bulletPool = null;
	}


	static public void UpdateProfileList(int _id, string _name) {
		GameManager.profiles[_id].player_id = _id;
		GameManager.profiles[_id].name = _name;
	}

}