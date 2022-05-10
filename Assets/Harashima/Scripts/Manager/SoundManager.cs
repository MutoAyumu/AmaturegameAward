using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CriAtomSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("���̃R���|�[�l���g��AudioSource")]
    AudioSource _audioSource;

    [SerializeField]
    CriAtomSource _criAtomSource;

    /// <summary>
    /// AudioClip��炷�֐�
    /// </summary>
    /// <param name="clip">�炵����AudioClip</param>
    public void SoundPlay(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void BGMPlay()
    {
        CriAtomPlay(CueSheet.BGM, "BGMField");
    }
    /// <summary>
    /// ADX�t�@�C�����Đ�����֐�
    /// </summary>
    /// <param name="cueSheet">�炷���̎�ނ�enum����I��</param>
    /// <param name="cueName">�炵�������̃t�@�C����</param>
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
    /// Source���w�肵��ADX�t�@�C�����Đ�����֐�
    /// </summary>
    /// <param name="source">�w�肷��Source</param>
    /// <param name="cueSheet">�炷���̎�ނ�enum����I��</param>
    /// <param name="cueName">�炵�������̃t�@�C����</param>
    public void CriAtomPlay(CriAtomSource source, CueSheet cueSheet, string cueName)
    {
        if (!source)
        {
            Debug.LogError("�g�p���CriAtomSource��null����");
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




