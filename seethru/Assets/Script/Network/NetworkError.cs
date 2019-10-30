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
        if (_errid >= 3)
        {
            msgChild = msgesParent.transform.GetChild(2).gameObject;
        }
        else
        {
            msgChild = msgesParent.transform.GetChild(_errid - 1).gameObject;
        }

        msgChild.SetActive(true);

        g_errorNumber = _errid;
    }


    public void PressOkButton()
    {
        msgChild.SetActive(false);                          // エラーメッセージを非アクティブしてリセット
        myObj.transform.parent.gameObject.SetActive(false); // ErrorWindowの親のCanvasごと非アクティブ
    
        switch (g_errorNumber)
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

            // default : その他のエラー　タイトルへ戻す
            default: { mrsClient.BackToTitle(); } break;
        }
    }

}
