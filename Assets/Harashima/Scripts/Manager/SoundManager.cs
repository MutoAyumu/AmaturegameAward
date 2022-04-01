using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("���̃R���|�[�l���g��AudioSource")]
    AudioSource _audioSource;

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
        _audioSource.Play();
    }
}
