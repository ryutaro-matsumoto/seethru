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
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ButtonProduction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image { get { return GetComponent<Image>(); } }

    //===========================================================
    // OnPointerEnter function.
    //
    // @note：オブジェクトの範囲内にマウスポインタが入った時
    //===========================================================
    public void OnPointerEnter(PointerEventData eventData)
    {
        // image.color = Color.white;
        image.color = new Color(0.6f, 1.0f, 1.0f, 1.0f);

    }

    //===========================================================
    // OnPointerExit function.
    //
    // @note：オブジェクトの範囲内からマウスポインタが出た時
    //===========================================================
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
