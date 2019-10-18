using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace DataStructures
{
    /// <summary>
    /// プレイヤーのプロファイル構造体
    /// </summary>
    public struct S_DataProfile
    {
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        //public string name;

        public int player_id;
        public int spawn_id;
    }

    /// <summary>
    /// プレイヤーの座標等データ
    /// </summary>
    public struct S_DataPlayer
    {
        public uint id;
        public float x, y;
        public float angle;
        public int bullets;
        public bool died;
    }

    /// <summary>
    /// 弾の座標等データ
    /// </summary>
    public struct S_DataShots
    {
        public float x, y;
        public float angle;
        public bool died;
    }

    /// <summary>
    ///  ゲーム開始用の初期化データ構造体
    /// </summary>
    public struct S_StartingData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] spawnid;
        public uint sumplayer;

    }

}