//------------------------------------------------------------------------------
// @name：TitleSystem.cs
//
// @note：
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
// TitleSystem class.
//------------------------------------------------------------------------------
public class TitleSystem : MonoBehaviour
{
    public AudioSource ClicjGameStartSound;

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
        ClicjGameStartSound.PlayOneShot(ClicjGameStartSound.clip);
        FadeManeger.Fadeout("ProtoScene");
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