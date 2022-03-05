using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("このコンポーネントのAudioSource")]
    AudioSource _audioSource;

    /// <summary>
    /// AudioClipを鳴らす関数
    /// </summary>
    /// <param name="clip">鳴らしたいAudioClip</param>
    public void SoundPlay(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
    }
}
