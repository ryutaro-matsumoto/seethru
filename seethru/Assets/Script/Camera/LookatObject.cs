using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatObject : MonoBehaviour
{	
	public uint livePlayerNum;
	GameObject[] livePlayers;

	private void Start() {
		livePlayerNum = GameManager.playerNum;
		livePlayers = new GameObject[livePlayerNum];
	}

	// Update is called once per frame
	void Update()
    {
		GameObject[] playerx = { null, null };
		GameObject[] playery = { null, null };

		float distancex = 0;
		float distancey = 0;

		for(int i = 0; i < GameManager.playerNum - 1; ++i){
			if(GameManager.players[i].GetComponent<Player>().isDead){
				continue;
			}
			
			for(int j = i + 1; j < GameManager.playerNum; ++j){
				if (GameManager.players[j].GetComponent<Player>().isDead) {
					continue;
				}

				Vector2 wvec = GameManager.players[j].transform.position - GameManager.players[i].transform.position;
				if (distancex < Mathf.Abs(wvec.x)) {
					playerx[0] = GameManager.players[i];
					playerx[1] = GameManager.players[j];
					distancex = Mathf.Abs(wvec.x);
				}
				if (distancey < Mathf.Abs(wvec.y)) {
					playery[0] = GameManager.players[i];
					playery[1] = GameManager.players[j];
					distancey = Mathf.Abs(wvec.y);
				}
			}
		}

		Vector2 vecx = Vector2.Lerp(playerx[0].transform.position, playerx[1].transform.position, 0.5f);
		Vector2 vecy = Vector2.Lerp(playery[0].transform.position, playery[1].transform.position, 0.5f);

		Vector2 newPosition = new Vector2(vecx.x, vecy.y);

		transform.position = newPosition;

    }
}
