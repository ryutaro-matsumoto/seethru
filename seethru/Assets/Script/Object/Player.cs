using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	/// <summary>
	/// プレイヤーのオイラー角
	/// </summary>
	public float angle;
	public int bullet = 3;
	public bool attackStop = false;

	public bool isDead = false;


    // Update is called once per frame
    void FixedUpdate() {
        if(isDead){
			DeadPlayer();
		}
    }

	void DeadPlayer(){
		gameObject.SetActive(false);
	}
	
}
