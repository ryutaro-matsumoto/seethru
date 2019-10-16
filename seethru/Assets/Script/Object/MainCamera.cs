using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	GameObject player;

	[Range(0.0f, 1.0f)]
	public float t;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("myplayer" + GameManager.playID);
    }

    // Update is called once per frame
    void LateUpdate()
    {
		Vector3 newPosition = transform.position;
		newPosition = Vector3.Lerp(transform.position, player.transform.position, t);

		newPosition.z = transform.position.z;

		transform.position = newPosition;


    }
}
