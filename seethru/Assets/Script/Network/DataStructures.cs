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
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        //public string name;

        public int player_id;
        public int spawn_id;
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
}