using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSetting : MonoBehaviour
{
    protected static String g_ArgConnectionType;
    protected static String g_ArgIsKeyExchange;
    protected static String g_ArgIsEncryptRecords;
    protected static String g_ArgWriteDataLen;
    protected static String g_ArgWriteCount;
    protected static String g_ArgConnections;
    protected static String g_ArgServerAddr;
    protected static String g_ArgServerPort;
    protected static String g_ArgTimeoutMsec;
    protected static String g_ArgIsValidRecord;
    protected static String g_ArgConnectionPath;

    NetworkSettingData netsettings;

    static NetworkSetting()
    {
#if UNITY_WEBGL
        g_ArgConnectionType   = "3";
        g_ArgIsKeyExchange    = "0";
#else
        g_ArgConnectionType = "2";
        g_ArgIsKeyExchange = "1";
#endif
        g_ArgIsEncryptRecords = "1";
        g_ArgWriteDataLen = "1024";
        g_ArgWriteCount = "10";
        g_ArgConnections = "1";
        g_ArgServerAddr = "127.0.0.1";
#if UNITY_WEBGL
        g_ArgServerPort       = "22223";
#else
        g_ArgServerPort = "22222";
#endif
        g_ArgTimeoutMsec = "5000";
        g_ArgIsValidRecord = "1";
        g_ArgConnectionPath = "/";
    }


    private void OnGUI()
    {
        if(netsettings != null) { netsettings = null; }
        netsettings = new NetworkSettingData();
        GUILayout.Space(30);

        GUILayout.BeginHorizontal();
        GUILayout.Label("ServerAddr:");
        g_ArgServerAddr = GUILayout.TextField(g_ArgServerAddr);
        GUILayout.Space(30);
        GUILayout.Label("ServerPort:");
        g_ArgServerPort = GUILayout.TextField(g_ArgServerPort);
        GUILayout.Space(30);
        GUILayout.Label("TimeoutMsec:");
        g_ArgTimeoutMsec = GUILayout.TextField(g_ArgTimeoutMsec);
        GUILayout.EndHorizontal();
        GUILayout.Space(60);
        
        
        if (GUILayout.Button("Start Game", GUILayout.Width(300), GUILayout.Height(80)))
        {
            netsettings.SetData(g_ArgServerAddr, g_ArgServerPort);
            mrs.Utility.LoadScene("SampleScene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
