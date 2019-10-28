//------------------------------------------------------------------------------
// @name：ButtonSystem.cs
//
// @note：
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// namespace declaration.
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//------------------------------------------------------------------------------
// ButtonSystem class.
//------------------------------------------------------------------------------
[RequireComponent(typeof(Image))]
public class ButtonSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // カーソルImage
    public Image image { get { return GetComponent<Image>(); } }

    // 選択SE
    public AudioSource selectSound;


    //===========================================================
    // OnPointerEnter function.
    //
    // @note：オブジェクトの範囲内にマウスポインタが入った時
    //===========================================================
    public void OnPointerEnter(PointerEventData eventData)
    {
       // image.color = Color.white;
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);

        SoundManager.Instance.PlaySe("SE1_Corsor");
       // selectSound.PlayOneShot(selectSound.clip);
    }

    //===========================================================
    // OnPointerExit function.
    //
    // @note：オブジェクトの範囲内からマウスポインタが出た時
    //===========================================================
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.clear;
    }

    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    void Start()
    {
    }

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {
    }
}
