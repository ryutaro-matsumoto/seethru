using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    string[] playerName;
    bool[] readyFlag;

    int myID = 0;

    GameObject playerList;

    MrsClient mrsClient;

    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.Find("PlayerList");
        playerName = new string[4];
        readyFlag = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            playerName[i] = "";
            readyFlag[i] = false;
        }
        playerName[0] = "hostman";


        mrsClient = GameObject.Find("ClientObject").GetComponent<MrsClient>();
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    void UpdateNameList()
    {
        for(int i = 0; i < 4; i++)
        {
            if (playerName[i] == "") { playerList.transform.GetChild(i).GetComponent<Text>().text = "Waiting other player..."; }
            else { playerList.transform.GetChild(i).GetComponent<Text>().text = playerName[i]; }
        }
    }

    public void PressReady()
    {
        readyFlag[myID] = true;
        bool not_ready = false;
        //for (int i = 0; i < 4; i++)
        //{
        //    if (playerName[i] != "")
        //    {
        //        if (!readyFlag[i])
        //        {
        //            not_ready = true;
        //            break;
        //        }
        //    }
        //}
        //UpdateNameList();
        if (!not_ready)
        {
            mrsClient.SendRoomReady();
        }
    }
}
