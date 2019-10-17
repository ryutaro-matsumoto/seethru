﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace DataStructures
{
    public struct S_DataProfile
    {
        public int player_id;
        //public string name;
        public int spawn_id;
    }

    public struct S_DataPlayer
    {
        public float x, y;
        public float angle;
        public int bullets;
        public bool died;
    }

    public struct S_DataShots
    {
        public float x, y;
        public float angle;
        public bool died;
    }

    public struct S_StartingData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] spawnid;
        public uint sumplayer;

    }

    public class Data_Structures : MonoBehaviour
    {
		
    }
}