﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataStructures;


public class NetworkSetting : MonoBehaviour
{
    string ipAddress = "127.0.0.1";
    string playerName = "Anonymous";

    // コンポーネント
    public MrsClient mrsClient;
    public InputField inputAddr;
    public InputField inputName;


    private void Start()
    {
    }

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

    /// <summary>
    /// JoinServerボタンが押されるとMrsの接続が呼び出される
    /// </summary>
    public void JoinServer()
    {
        this.gameObject.SetActive(false);

        mrsClient = mrsClient.GetComponent<MrsClient>();
        mrsClient.SetSettings(ipAddress, playerName);
        mrsClient.StartEchoClient();

        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// IPアドレスの入力ボックス
    /// </summary>
    public void SetAddress()
    {
        ipAddress = inputAddr.text;
    }

    /// <summary>
    /// 名前の入力ボックス
    /// </summary>
    public void SetName()
    {
        playerName = inputName.text;
    }
}