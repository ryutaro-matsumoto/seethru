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

	public string gameStart;

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        // フェードイン
        FadeManeger.Fadein();

        // タイトルBGMスタート
        SoundManager.Instance.PlayBgm("BGM_Title");
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
        // 決定SE再生
        SoundManager.Instance.PlaySe("SE1_Enter");
        //clickEnter.PlayOneShot(clickEnter.clip);
        // フェードアウト
        FadeManeger.Fadeout(gameStart);
        // タイトルBGMストップ
        //SoundManager.Instance.StopBgm();
        SoundManager.Instance.StopBgmFadeout();
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