using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataStructures;


public class NetworkError : MonoBehaviour
{
    MrsClient mrsClient;
    GameObject myObj;
    GameObject msgesParent;
    GameObject msgChild;

    int g_errorNumber;

    private void Awake()
    {
        mrsClient = GameManager.connection;
        myObj = this.gameObject;
        msgesParent = GameObject.Find("ErrorMessages");
    }


    /// <summary>
    /// ネットワークエラーのウィンドウを表示
    /// </summary>
    /// <param name="_errid">エラー番号</param>
    public void PopupErrorWindow(int _errid)
    {
        g_errorNumber = _errid - 1;
        msgChild = msgesParent.transform.GetChild(g_errorNumber).gameObject;
        msgChild.SetActive(true);
    }


    public void PressOkButton()
    {
        msgChild.SetActive(false);                          // エラーメッセージを非アクティブしてリセット
        myObj.transform.parent.gameObject.SetActive(false); // ErrorWindowの親のCanvasごと非アクティブ
    
        switch (g_errorNumber + 1)
        {
            // case 1 : とりあえずネットワークのエラー　タイトルへ戻す
            case 1:
                {
                    mrsClient.BackToTitle();
                }
                break;

            // case 2 : 入力したIPアドレスの接続先が見つからない　ウィンドウだけ
            case 2:
                {

                }
                break;

            default: { }break;
        }
    }

}
