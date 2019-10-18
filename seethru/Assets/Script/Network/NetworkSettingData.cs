using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructures;
using System.Runtime.InteropServices;

public class NetworkSettingData : MonoBehaviour
{
    public string addr;
    public ushort port;
    public IntPtr connection;

    public S_DataProfile myProfile;


    void Start()
    {

    }

    public void SetProfile(int _playerid, string _name, int _spawnid)
    {
        myProfile.player_id = _playerid;
        //myProfile.name = _name;
        myProfile.spawn_id = _spawnid;
        Console.WriteLine("SetProfile id:{0} ", myProfile.player_id);
    }

    public S_DataProfile GetMyProfile()
    {
        return myProfile;
    }
}
