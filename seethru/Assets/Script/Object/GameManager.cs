using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool onNetwork = false;


	[HideInInspector]
	private MrsClient connection;
	public uint playerNum = 0;
	public GameObject[] players;
	public uint playID;
	public GameObject bulletPool = null;

	private void Start() {
		connection = GameObject.Find("ClientObject").GetComponent<MrsClient>();

		GameObject otherPlayerPrefab = (GameObject)Resources.Load("Object/OtherPlayer");

		if (connection != null){
			connection.InitMrsforGame();
			onNetwork = true;
		}

		else{
			playID = 0;
			players[0] = Instantiate(mainPlayerPrefab);
			for(int i = 1; i < 4; ++i){
				players[i] = Instantiate(otherPlayerPrefab);
			}
		}
	}

	public GameObject InstantiateMainPlayer(uint id){
		GameObject mainPlayerPrefab = (GameObject)Resources.Load("Object/MainPlayer");
		players[id] = Instantiate(mainPlayerPrefab, Vector3.zero, Quaternion.identity);

		playID = id;
		return players[id];
	}
}
