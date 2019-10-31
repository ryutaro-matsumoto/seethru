
//------------------------------------------------------------------------------
// namespace declaration.
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
// ResultSystem class.
//------------------------------------------------------------------------------
public class ResultSystem : MonoBehaviour
{
    // ウィンドウイメージ
    public Image image;

	public string backTitle = "TitleScene";
	public string backRoom = "MatchRoom";

    // スタート時BGM/SE再生
    IEnumerator Playsound()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayBgm("BGM_Result");
    }

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        StartCoroutine(Playsound());
        image.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);


	}

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {
        
    }

    //===========================================================
    // ClickRoomBack function.
    //
    // @note：「ルームに戻る」ボタンをクリック時処理
    //===========================================================
    public void ClickRoomBack()
    {
		GameManager.BackRoom();
	}

    //===========================================================
    // ClickExitButton function.
    //
    // @note：「ルームからでる」ボタンをクリック時処理
    //===========================================================
    public void ClickRoomExit()
    {
		if(GameManager.onNetwork){
			GameManager.BackTitle();
		}
		else{
			// フェードアウト
			FadeManeger.Fadeout(backTitle);
		}
		GameManager.connection.DisconnectRoom();
	}
}
