using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Audio管理クラス。シーンをまたいでも破棄されないシングルトン。
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private AudioSource _audioSource;
    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(null != instance)
        {
            // すでにインスタンスがある場合は自信を破棄する
            Destroy(gameObject);
            return;
        }


        // シーンをまたいでも破棄されないようにする
        DontDestroyOnLoad(gameObject);
        instance = this;

        // Resource/2D_SEディレクトリ下のAudioClipをすべて取得する
        var audioClips = Resources.LoadAll<AudioClip>("2D_SE");

        foreach(var clip in audioClips)
        {
            _clips.Add(clip.name, clip);
        }

    }

    public void Play(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            throw new Exception("Sound " + clipName + " is not defined.");
        }

        _audioSource.clip = _clips[clipName];
        _audioSource.Play();
    }
}
