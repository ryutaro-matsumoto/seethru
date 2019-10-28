using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitProto : MonoBehaviour
{
	public uint stagenum;
	public uint id;
	public Guard.GuardColliderType[] guards = new Guard.GuardColliderType[2];

    // Start is called before the first frame update
    void Start()
    {
		GameManager.ReceiveID(id);
		int[] table = { 0, 1, 2, 3 };

		GameManager.guards = guards;

		GameManager.GameStart(stagenum, table, 4);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
