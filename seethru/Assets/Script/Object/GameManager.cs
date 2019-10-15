using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool onNetwork = false;

	[HideInInspector]
	private MrsClient connection;
	public uint playerNum = 0;
	public GameObject mainPlayer = null;
	public GameObject[] otherPlayer;
	public GameObject bulletPool = null;

	private void Start() {
		connection = GameObject.Find("ClientObject").GetComponent<MrsClient>();

		if(connection != null){
			onNetwork = true;
			// connection.Init();
		}

		/*
		 
		 
		 
		 */


		Debug.Assert(playerNum < 2);

		otherPlayer = new GameObject[playerNum - 1];
		
		/*
		 * 
		 * 
		 * player初期位置セット
		 *
		 * 
		 *
		 */


	}

}
