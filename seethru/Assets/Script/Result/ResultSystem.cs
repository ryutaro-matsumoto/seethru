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

//------------------------------------------------------------------------------
// ResultSystem class.
//------------------------------------------------------------------------------
public class ResultSystem : MonoBehaviour
{
    // 決定SE
    public AudioSource ClickEnter;
    // リザルトメインBGM
    public AudioSource resultSound;
    // ウィンドウSE
    public AudioSource resultmenuSound;

    // スタート時BGM/SE再生
    IEnumerator playSound()
    {
        yield return new WaitForSeconds(0.1f);
        resultmenuSound.PlayOneShot(resultmenuSound.clip);
        yield return new WaitForSeconds(1f);
        resultSound.PlayOneShot(resultSound.clip);
    }

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
        StartCoroutine(playSound());
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
        ClickEnter.PlayOneShot(ClickEnter.clip);
        FadeManeger.Fadeout("ResultScene");
    }

    //===========================================================
    // ClickExitButton function.
    //
    // @note：「ルームからでる」ボタンをクリック時処理
    //===========================================================
    public void ClickRoomExit()
    {
        ClickEnter.PlayOneShot(ClickEnter.clip);
        FadeManeger.Fadeout("TitleScene");
    }
}
