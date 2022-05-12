using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        CriAtomPlay(CueSheet.BGM, "BGMField");
    }

    public void FadeInAudio(float time)
    {
        var volume = 0;
        DOVirtual.Float(volume, 1, time, volume => _criAtomSource.volume = volume);
    }
    public void FadeOutAudio(float time)
    {
        var volume = _criAtomSource.volume;
        DOVirtual.Float(volume, 0, time, volume => _criAtomSource.volume = volume);
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
    /// <summary>
    /// Sourceを指定してADXファイルを再生する関数
    /// </summary>
    /// <param name="source">指定するSource</param>
    /// <param name="cueSheet">鳴らす音の種類をenumから選択</param>
    /// <param name="cueName">鳴らしたい音のファイル名</param>
    public void CriAtomPlay(CriAtomSource source, CueSheet cueSheet, string cueName)
    {
        if (!source)
        {
            Debug.LogError("使用先のCriAtomSourceがnullだよ");
            return;
        }
        source.cueSheet = cueSheet.ToString();
        source.cueName = cueName;
        source.Play();
    }
}

public enum CueSheet
{
    BGM,
    SE,
    ME
}




