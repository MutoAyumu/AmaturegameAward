using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CriAtomSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("このコンポーネントのAudioSource")]
    AudioSource _audioSource;

    [SerializeField]
    CriAtomSource _criAtomSource;

    /// <summary>
    /// AudioClipを鳴らす関数
    /// </summary>
    /// <param name="clip">鳴らしたいAudioClip</param>
    public void SoundPlay(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void BGMPlay()
    {
        _audioSource.Play();
    }

    /// <summary>
    /// ADXファイルを再生する関数
    /// </summary>
    /// <param name="cueSheet">鳴らす音の種類をenumから選択</param>
    /// <param name="cueName">鳴らしたい音のファイル名</param>
    public void CriAtomPlay(CueSheet cueSheet,string cueName)
    {
        if(!_criAtomSource)
        {
            _criAtomSource = GetComponent<CriAtomSource>();
        }
        _criAtomSource.cueSheet = cueSheet.ToString();
        _criAtomSource.cueName = cueName;
        _criAtomSource.Play();
    }
}

public enum CueSheet
{
    BGM,
    SE
}




