using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkSetting : MonoBehaviour
{
    public string ipAddress = "192.168.252.124";
    public string portNummber = "22222";



    //private void OnGUI()
    //{
    //    if (!try2connect)
    //    {
    //        if (netsettings != null) { netsettings = null; } 
    //        GUILayout.Space(30);

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("ServerAddr:");
    //        g_ArgServerAddr = GUILayout.TextField(g_ArgServerAddr);
    //        GUILayout.Space(30);
    //        GUILayout.Label("ServerPort:");
    //        g_ArgServerPort = GUILayout.TextField(g_ArgServerPort);
    //        GUILayout.Space(30);
    //        GUILayout.Label("TimeoutMsec:");
    //        g_ArgTimeoutMsec = GUILayout.TextField(g_ArgTimeoutMsec);
    //        GUILayout.EndHorizontal();
    //        GUILayout.Space(60);

    //        if (GUILayout.Button("Start Game", GUILayout.Width(300), GUILayout.Height(80)))
    //        {
    //            MrsClient client = gameObject.GetComponent<MrsClient>();
    //            netsettings = new NetworkSettingData();
    //            try2connect = true;
    //            trytime = Int32.Parse(g_ArgTimeoutMsec)+500;
    //            client.StartEchoClient();
    //            //mrs.Utility.LoadScene("SampleScene");
    //        }
    //    }
    //    else
    //    {
    //        GUILayout.TextArea("Connecting ...");
    //    }
    //}

    void End()
    {
    }

    void OnDestroy()
    {
        End();
    }

    // Update is called once per frame
    void Update()
    {

    }

}