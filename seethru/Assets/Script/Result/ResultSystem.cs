//------------------------------------------------------------------------------
// @name：ResultSystem.cs
//
// @note：
//------------------------------------------------------------------------------

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
	public string backRoom = "MatchRoom";

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
        clickEnter.PlayOneShot(clickEnter.clip);
        FadeManeger.Fadeout(backRoom);
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
		GameManager.connection.DisconnectRoom();
    }
}
