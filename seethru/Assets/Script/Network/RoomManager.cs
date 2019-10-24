using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    DataStructures.S_DataProfile[] profile;

    string[] playerName;
    bool[] readyFlag;

    int myID = 0;

    GameObject playerList;

    MrsClient mrsClient;

    

    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.Find("PlayerList");
        profile = new DataStructures.S_DataProfile[4];
        playerName = new string[4];
        readyFlag = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            profile[i] = new DataStructures.S_DataProfile();
            profile[i].player_id = i;
            profile[i].name = "";
            readyFlag[i] = false;
        }
        playerName[0] = "hostman";

        mrsClient = GameObject.Find("ClientObject").GetComponent<MrsClient>();
        mrsClient.setRoomManager(this);
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void UpdateNameList()
    {
        for(int i = 0; i < 4; i++)
        {
            if (profile[i].name == "") { playerList.transform.GetChild(i).GetComponent<Text>().text = "Waiting other player..."; }
            else { playerList.transform.GetChild(i).GetComponent<Text>().text = profile[i].name; }
            if(myID == i) { playerList.transform.GetChild(i).GetComponent<Text>().text = "ITS ME"; }
        }
    }

    public void UpdateProfileList(int _id, string _name)
    {
        profile[_id].player_id = _id;
        profile[_id].name = _name;
        UpdateNameList();
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

    public void setMyID(int _id) { myID = _id; }
}
