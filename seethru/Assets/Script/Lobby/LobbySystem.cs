﻿//------------------------------------------------------------------------------
// @name：LobbySystem.cs
//
// @note：
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// namespace declaration.
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//------------------------------------------------------------------------------
// LobbySystem class.
//------------------------------------------------------------------------------
public class LobbySystem : MonoBehaviour
{
    //\ 戻るSE
    public AudioSource ClickBackSound;


    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        FadeManeger.Fadein();

    }

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {
     
    }


    //===========================================================
    // ClickCreateRoomButton function.
    //
    // @note：「ルームを作成」ボタンをクリック時処理
    //===========================================================
    public void ClickCreateRoomButton()
    {

    }

    //===========================================================
    // ClickJoinRoomButton function.
    //
    // @note：「ルームに入る」ボタンをクリック時処理
    //===========================================================
    public void ClickJoinRoomButton()
    {

    }

    //===========================================================
    // ClickBackButton function.
    //
    // @note：「戻る」ボタンをクリック時処理
    //===========================================================
    public void ClickBackButton()
    {
        ClickBackSound.PlayOneShot(ClickBackSound.clip);
        FadeManeger.Fadeout("TitleScene");
    }
}