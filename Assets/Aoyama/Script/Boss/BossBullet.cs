using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _muzzle;
    [Header("‰¹")]
    [SerializeField] string _cuename = "GaBossTama";

    public void Instantiate()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cuename);
        Instantiate(_bullet, _muzzle.position, Quaternion.identity);
    }

}
