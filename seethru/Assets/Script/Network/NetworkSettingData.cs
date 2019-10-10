using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MrsServer = System.IntPtr;
using MrsConnection = System.IntPtr;
using MrsCipher = System.IntPtr;

public class NetworkSettingData : Mrs
{
    public static string addr;
    public static string port;
    public static MrsConnection connection;

    public void SetData(string _addr, string _port)
    {
        addr = _addr;
        port = _port;
    }

    static NetworkSettingData()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
