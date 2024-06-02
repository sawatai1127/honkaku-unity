using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Audio�Ǘ��N���X�B�V�[�����܂����ł��j������Ȃ��V���O���g���B
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
            // ���łɃC���X�^���X������ꍇ�͎��M��j������
            Destroy(gameObject);
            return;
        }


        // �V�[�����܂����ł��j������Ȃ��悤�ɂ���
        DontDestroyOnLoad(gameObject);
        instance = this;

        // Resource/2D_SE�f�B���N�g������AudioClip�����ׂĎ擾����
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
