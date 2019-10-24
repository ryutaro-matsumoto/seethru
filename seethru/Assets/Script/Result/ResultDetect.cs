using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        if(GameManager.players[GameManager.playID].GetComponent<Player>().isDead){
			transform.GetChild(0).gameObject.SetActive(true);
		}
		else{
			transform.GetChild(1).gameObject.SetActive(true);
		}
	}

}
