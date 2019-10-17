using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSprite : MonoBehaviour
{
	TrailRenderer tr;

    // Start is called before the first frame update
    void OnEnable()
    {
		tr = GetComponent<TrailRenderer>();
		tr.Clear();

		Debug.Log("clear");
    }
}
