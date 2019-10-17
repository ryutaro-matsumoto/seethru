using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatObject : MonoBehaviour
{	
	float distancey;

	Vector2 distance;

	public Vector2 Distance { get { return distance; } }


	// Update is called once per frame
	void Update()
    {
		GameObject[] playerx = { null, null };
		GameObject[] playery = { null, null };

		distance = Vector2.zero;

		for(int i = 0; i < GameManager.playerNum - 1; ++i){
			if(GameManager.players[i].GetComponent<Player>().isDead){
				continue;
			}
			
			for(int j = i + 1; j < GameManager.playerNum; ++j){
				if (GameManager.players[j].GetComponent<Player>().isDead) {
					continue;
				}

				Vector2 wvec = GameManager.players[j].transform.position - GameManager.players[i].transform.position;
				if (distance.x < Mathf.Abs(wvec.x)) {
					playerx[0] = GameManager.players[i];
					playerx[1] = GameManager.players[j];
					distance.x = Mathf.Abs(wvec.x);
				}
				if (distance.y < Mathf.Abs(wvec.y)) {
					playery[0] = GameManager.players[i];
					playery[1] = GameManager.players[j];
					distance.y = Mathf.Abs(wvec.y);
				}
			}
		}

		Vector2 vecx = Vector2.Lerp(playerx[0].transform.position, playerx[1].transform.position, 0.5f);
		Vector2 vecy = Vector2.Lerp(playery[0].transform.position, playery[1].transform.position, 0.5f);

		Vector2 newPosition = new Vector2(vecx.x, vecy.y);

		transform.position = newPosition;
    }
}
