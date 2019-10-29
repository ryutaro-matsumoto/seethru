
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
    // 決定SE
    public AudioSource clickEnter;
    // リザルトメインBGM
    public AudioSource resultSound;
    // ウィンドウSE
    public AudioSource resultwindowSound;
    // ウィンドウイメージ
    public Image image;

	public string backTitle = "TitleScene";

    // スタート時BGM/SE再生
    IEnumerator Playsound()
    {
        yield return new WaitForSeconds(0.1f);
        resultwindowSound.PlayOneShot(resultwindowSound.clip);
        yield return new WaitForSeconds(1f);
        resultSound.PlayOneShot(resultSound.clip);
    }

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        StartCoroutine(Playsound());
        image.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
		GameManager.isGetMyProfile = false;
    }

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {
		if (GameManager.isGetMyProfile) {
			FadeManeger.Fadeout("MatchRoom");
			gameObject.SetActive(false);
		}
	}

	//===========================================================
	// ClickRoomBack function.
	//
	// @note：「ルームに戻る」ボタンをクリック時処理
	//===========================================================
	public void ClickRoomBack()
    {
        clickEnter.PlayOneShot(clickEnter.clip);
        GameManager.connection.backToRoom();
		GameManager.BackRoom();
    }

    //===========================================================
    // ClickExitButton function.
    //
    // @note：「ルームからでる」ボタンをクリック時処理
    //===========================================================
    public void ClickRoomExit()
    {
        clickEnter.PlayOneShot(clickEnter.clip);
        FadeManeger.Fadeout(backTitle);
		if(GameManager.onNetwork){
			GameManager.connection.DisconnectRoom();
		}
	}
}
