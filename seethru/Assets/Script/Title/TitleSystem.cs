//------------------------------------------------------------------------------
// @name：TitleSystem.cs
//
// @note：
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// namespace declaration.
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//------------------------------------------------------------------------------
// TitleSystem class.
//------------------------------------------------------------------------------
public class TitleSystem : MonoBehaviour
{
    // 決定SE
    public AudioSource clickEnter;

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        // フェードイン
        FadeManeger.Fadein();
    }

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {

    }

    //===========================================================
    // Quit function.
    //
    // @note：ゲーム終了
    //===========================================================
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    //===========================================================
    // ClickGameStartButton function.
    //
    // @note：「Game Start」ボタンをクリック時処理
    //===========================================================
    public void ClickGameStartButton()
    {
        clickEnter.PlayOneShot(clickEnter.clip);
        FadeManeger.Fadeout("ResultScene");
    }

    //===========================================================
    // ClickExitButton function.
    //
    // @note：「Exit」ボタンをクリック時処理
    //===========================================================
    public void ClickExitButton()
    {
        Quit();
    }
}