using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitProto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameManager.ReceiveID(0);
		int[] table = { 0, 1, 2, 3 };
		GameManager.ProtoStart(table, 4);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
