//------------------------------------------------------------------------------
// @name：SoudManager.cs
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

// シーンに存在するかチェックだよ
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "がシーンに存在しません。");
                }
            }
            return instance;
        }
    }
}

//------------------------------------------------------------------------------
// SoundManager class.
//------------------------------------------------------------------------------
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField, Range(0, 1), Tooltip("マスタ音量")]
    float volume = 1;
    [SerializeField, Range(0, 1), Tooltip("BGMの音量")]
    float bgmVolume = 1;
    [SerializeField, Range(0, 1), Tooltip("SEの音量")]
    float seVolume = 1;
    AudioClip[] bgm;
    AudioClip[] se;
    Dictionary<string, int> bgmIndex = new Dictionary<string, int>();
    Dictionary<string, int> seIndex = new Dictionary<string, int>();
    AudioSource bgmAudioSource;
    AudioSource seAudioSource;


	public enum BGMIndex{
		GameMain = 0,
		Network,
		Result,
		Room,
		Titie
	};

	public enum SEIndex{
		Corsor = 0,
		Enter,
		ResultWindow,
		Drop,
		Hit,
		Hit2,
		Reflection,
		Shot
	}

    public double fadeTime = 1.0;
    bool isfadeOut = false;
    double fadeDeltaTime = 0;

    public float Volume
    {
        set
        {
            volume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
            seAudioSource.volume = seVolume * volume;
        }
        get
        {
            return volume;
        }
    }
    public float BgmVolume
    {
        set
        {
            bgmVolume = Mathf.Clamp01(value);
            bgmAudioSource.volume = bgmVolume * volume;
        }
        get
        {
            return bgmVolume;
        }
    }
    public float SeVolume
    {
        set
        {
            seVolume = Mathf.Clamp01(value);
            seAudioSource.volume = seVolume * volume;
        }
        get
        {
            return seVolume;
        }
    }
    //------------------------------------------------------------------------------
    // start function.
    //------------------------------------------------------------------------------
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();
        bgm = Resources.LoadAll<AudioClip>("Audio/BGM");
        se = Resources.LoadAll<AudioClip>("Audio/SE");
        for (int i = 0; i < bgm.Length; i++)
        {
            bgmIndex.Add(bgm[i].name, i);
        }
        for (int i = 0; i < se.Length; i++)
        {
            seIndex.Add(se[i].name, i);
        }

		GameManager.soundManager = this;

        // fadeTime = 1.0;
        //isfadeOut = false;
        //fadeDeltaTime = 0;
}

    //------------------------------------------------------------------------------
    // update function.
    //------------------------------------------------------------------------------
    public void Update()
    {
        // フェードアウト処理
        if (isfadeOut)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeTime)
            {
                fadeDeltaTime = fadeTime;
                isfadeOut = false;
                bgmAudioSource.Stop();
                bgmAudioSource.clip = null;
            }
            bgmAudioSource.volume = (float)(1.0 - fadeDeltaTime / fadeTime);
        }
    }

    public int GetBgmIndex(string name)
    {
        if (bgmIndex.ContainsKey(name))
        {
            return bgmIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のBGMファイルが存在しません。");
            return 0;
        }
    }
    public int GetSeIndex(string name)
    {
        if (seIndex.ContainsKey(name))
        {
            return seIndex[name];
        }
        else
        {
            Debug.LogError("指定された名前のSEファイルが存在しません。");
            return 0;
        }
    }

    //===========================================================
    //BGM再生
    //===========================================================
    public void PlayBgmInit(int index)
    {
        fadeTime = 2.0;
        isfadeOut = false;
        fadeDeltaTime = 0;

        index = Mathf.Clamp(index, 0, bgm.Length);
        bgmAudioSource.clip = bgm[index];
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = BgmVolume * Volume;
        bgmAudioSource.Play();

    }
    public void PlayBgm(string name)
    {
        PlayBgmInit(GetBgmIndex(name));
    }

    //===========================================================
    // BGM停止
    //===========================================================
    public void StopBgm()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }

    //===========================================================
    // フェードアウトしながらBGM停止
    //===========================================================
    public void StopBgmFadeout()
    {
        isfadeOut = true;
    }

    //===========================================================
    //SE再生
    //===========================================================
    public void PlaySeInit(int index)
    {
        index = Mathf.Clamp(index, 0, se.Length);
        seAudioSource.PlayOneShot(se[index], SeVolume * Volume);
    }

    public void PlaySe(string name)
    {
        PlaySeInit(GetSeIndex(name));
    }

    //===========================================================
    //SE停止
    //===========================================================
    public void StopSe()
    {
        seAudioSource.Stop();
        seAudioSource.clip = null;
    }
}