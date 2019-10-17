//------------------------------------------------------------------------------
// @name：FadeManeger.cs
//
// @note：関数を呼び出せば使用できます。
//          FadeManeger.Fadein;
//          FadeManeger.Fadeout("シーン名");
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
// namespace declaration.
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//------------------------------------------------------------------------------
// FadeManeger class.
//------------------------------------------------------------------------------
public class FadeManeger : MonoBehaviour
{
    //\ フェード用のCanvasとImage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    //\ フェード用Imageの透明度
    private static float alpha = 0.0f;

    //\ フェードインフラグ
    private static bool isFadein = false;
    //\ フェードアウトフラグ
    public static bool isFadeout = false;

    //\ フェードしたい時間
    private static float fadeTime = 1.5f;

    //\ 次のシーン番号
    private static int nextScene = 0;

    //\ 次のシーン名
    private static string nextSceneName;

    //===========================================================
    // Fadein function.
    // 
    // @note：関数を呼び出せば使用できます。
    //        FadeManeger.Fadein;
    //===========================================================
    public static void Fadein()  {
        if (fadeImage == null) Init();
        fadeImage.color = Color.black;
        isFadein = true;
    }

    //===========================================================
    // Fadeout function.
    // 
    // @note：関数を呼び出せば使用できます。
    //        FadeManeger.Fadeout("シーン名");
    //===========================================================
    public static void Fadeout(string sceneName) {
        if (fadeImage == null) Init();

        // ▼ シーン名を使うときはこちら
        nextSceneName = sceneName;
        // nextScene = n;

        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        isFadeout = true;
    }

    //===========================================================
    // GetSceneNumber function.
    //
    // @note：現在のシーン番号を取得（シーン番号で管理する場合）
    //===========================================================
    public static int GetSceneNumber() {
        return nextScene;
    }

    //------------------------------------------------------------------------------
    // Init function.
    //------------------------------------------------------------------------------
    static void Init()
    {
        // フェード用のCanvas作成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManeger>();

        fadeCanvas.sortingOrder = 100;

        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    void Update()
    {
        // フェードイン処理
        if (isFadein){
            alpha -= Time.deltaTime / fadeTime;

            if (alpha <= 0.0f)
            {
                isFadein = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }

        // フェードアウト処理
        else  if (isFadeout){
            alpha += Time.deltaTime / fadeTime;

            if (alpha >= 1.0f)
            {
                isFadeout = false;
                alpha = 1.0f;

                SceneManager.LoadScene(nextSceneName);
                //SceneManager.LoadScene(nextScene);
            }
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
