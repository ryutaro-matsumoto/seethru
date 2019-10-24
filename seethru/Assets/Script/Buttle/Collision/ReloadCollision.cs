using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCollision : MonoBehaviour
{

	Player player;


	// Start is called before the first frame update
	void Start()
    {
		player = transform.parent.GetComponent<Player>();  
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "ReloadZone"){
		}
	}
}
