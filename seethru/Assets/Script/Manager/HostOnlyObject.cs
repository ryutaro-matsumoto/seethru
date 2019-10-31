using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostOnlyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if(GameManager.playID != 0){
			gameObject.SetActive(false);
		}
    }
}
