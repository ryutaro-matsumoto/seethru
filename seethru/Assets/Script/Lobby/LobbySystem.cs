//------------------------------------------------------------------------------
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
    public  GameObject RoomCreate;
    public  GameObject RoomJoin;
    public  GameObject Back;

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
        RoomCreate.SetActive(true);
        RoomJoin.SetActive(false);
    }

    //===========================================================
    // ClickJoinRoomButton function.
    //
    // @note：「ルームに入る」ボタンをクリック時処理
    //===========================================================
    public void ClickJoinRoomButton()
    {
        RoomCreate.SetActive(false);
        RoomJoin.SetActive(true);
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
