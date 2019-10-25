using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataStructures;


public class NetworkSetting : MonoBehaviour
{

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

        mrsClient = GameObject.Find("ClientObject").GetComponent<MrsClient>();
        mrsClient.SetSettings(GameManager.ipAddress, GameManager.playerName);
        mrsClient.StartEchoClient();

        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// IPアドレスの入力ボックス
    /// </summary>
    public void SetAddress()
    {
        GameManager.ipAddress = inputAddr.text;
    }

    /// <summary>
    /// 名前の入力ボックス
    /// </summary>
    public void SetName()
    {
        GameManager.playerName = inputName.text;
    }
}