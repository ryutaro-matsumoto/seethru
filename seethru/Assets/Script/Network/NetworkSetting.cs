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
    public GameObject InputParent;
    public GameObject InputChild;

    string[] addr;


    private void Start()
    {
        //InputParent = GameObject.Find("InputAddress");
        //InputChild = InputParent.transform.GetChild(0).gameObject;

        addr = new string[4];
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

    public void SetAddress1()
    {
        InputChild = InputParent.transform.GetChild(0).transform.gameObject;
        addr[0] = InputParent.transform.GetChild(0).GetComponent<InputField>().text;
        if (addr[0].Length >= 3)
        {
            InputParent.transform.GetChild(1).GetComponent<InputField>().ActivateInputField();
            
        }
    }
    public void SetAddress2()
    {
        InputChild = InputParent.transform.GetChild(1).transform.gameObject;
        addr[1] = InputParent.transform.GetChild(1).GetComponent<InputField>().text;
        if (addr[0].Length >= 3)
        {
            InputParent.transform.GetChild(2).GetComponent<InputField>().ActivateInputField();
        }
    }
    public void SetAddress3()
    {

        InputChild = InputParent.transform.GetChild(2).transform.gameObject;
        addr[2] = InputParent.transform.GetChild(2).GetComponent<InputField>().text;
        if (addr[2].Length >= 3)
        {
            InputParent.transform.GetChild(3).GetComponent<InputField>().ActivateInputField();
        }
    }
    public void SetAddress4()
    {
        InputChild = InputParent.transform.GetChild(3).transform.gameObject;
        addr[0] = InputParent.transform.GetChild(3).GetComponent<InputField>().text;
        if (addr[0].Length >= 3)
        {
        }

    }

    /// <summary>
    /// 名前の入力ボックス
    /// </summary>
    public void SetName()
    {
        GameManager.playerName = inputName.text;
    }
}