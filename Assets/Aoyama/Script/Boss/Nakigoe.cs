using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nakigoe : MonoBehaviour
{
    [Header("��")]
    [SerializeField] string _cuename = "GaBossVoice";
    
    public void Voice()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cuename);
    }
}
