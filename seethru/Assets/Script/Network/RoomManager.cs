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

	GuardSetting gs;

    // Start is called before the first frame update
    void Start()
    {
		FadeManeger.Fadein();

        SoundManager.Instance.PlayBgm("BGM_Room");

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

		gs = GameObject.Find("GuardSetting").GetComponent<GuardSetting>();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateNameList();
	}

	public void UpdateNameList()
    {
        for(int i = 0; i < 4; i++)
        {
            if (GameManager.profiles[i].name == "") { playerList.transform.GetChild(i).GetComponent<Text>().text = "Waiting other player..."; }
            else { playerList.transform.GetChild(i).GetComponent<Text>().text = GameManager.profiles[i].name; }
            if(myID == i) { playerList.transform.GetChild(i).GetComponent<Text>().color = new Color(1f, 0f, 0f); }
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

        SoundManager.Instance.StopBgmFadeout();
    }

    public void setMyID(int _id) { myID = _id; }
}
