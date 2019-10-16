using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtleInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
		GameManager.PlayersInit();

		for(int i = 0; i < GameManager.playerNum; ++i){
			if(GameManager.playID == i){
				GameManager.players[i].name = "myplayer" + i;
				continue;
			}
			GameManager.players[i].name = "player" + i;
		}
    }

}
