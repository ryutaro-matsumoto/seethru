using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace DataStructures
{
    /// <summary>
    /// プレイヤーのプロファイル構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct S_DataProfile
    {
        public int player_id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string name;
    }

    /// <summary>
    /// プレイヤーの座標等データ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct S_DataPlayer
    {
        public uint id;
        public float x, y;
        public float angle;
        public bool dead;
    }

    /// <summary>
    /// 弾の座標等データ
    /// </summary>
    public struct S_DataShots
    {
        public int bullet_id;
        public uint whos_shot;
        public float x, y;
        public float angle;
    }

    /// <summary>
    ///  ゲーム開始用の初期化データ構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct S_StartingData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] spawnid;
        public uint sumplayer;
        public int stageid;
    }

    /// <summary>
    /// 4人分プレイヤーデータのパッケージ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct S_DataPlayerPackage
    {
        public S_DataPlayer data0;
        public S_DataPlayer data1;
        public S_DataPlayer data2;
        public S_DataPlayer data3;
    } 

    // 被弾死用データ構造体
    [StructLayout(LayoutKind.Sequential)]
    public struct S_DeadHit
    {
        public int player_id;
        public int bullet_id;
        public int whosby_id;
    }
}