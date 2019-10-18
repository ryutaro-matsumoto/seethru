﻿using System;
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

    /// <summary>
    ///  プレイヤープロファイルのセット
    /// </summary>
    /// <param name="_playerid">プレイヤーのID</param>
    /// <param name="_name">プレイヤーの名前</param>
    /// <param name="_spawnid">スポーンエリア番号</param>
    public void SetProfile(int _playerid, string _name, int _spawnid)
    {
        myProfile.player_id = _playerid;
        //myProfile.name = _name;
        myProfile.spawn_id = _spawnid;
        Console.WriteLine("SetProfile id:{0} ", myProfile.player_id);
    }

    /// <summary>
    /// 自分のプロファイルを構造体ごと引き渡す
    /// </summary>
    /// <returns>S_DataProfileで返す</returns>
    public S_DataProfile GetMyProfile()
    {
        return myProfile;
    }
}
