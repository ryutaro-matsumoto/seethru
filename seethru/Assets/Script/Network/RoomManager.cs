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

	StageSelect stageSelect;

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

		stageSelect = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<StageSelect>();

        mrsClient = GameObject.Find("ClientObject").GetComponent<MrsClient>();
        mrsClient.setRoomManager(this);
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void UpdateNameList(int _id, string _name)
    {
        playerName[_id] = _name;

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
            mrsClient.SendRoomReady(stageSelect.selectStage);
        }
    }
}
