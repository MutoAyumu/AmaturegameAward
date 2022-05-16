using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _muzzle;
    [Header("‰¹")]
    [SerializeField] CriAtomSource _criAtomSource;
    [SerializeField] string _cuename = "GaBossRinpun";

    public void Instantiate()
    {
        Instantiate(_bullet, _muzzle.position, Quaternion.identity);
    }

    public void BulletSound()
    {
        _criAtomSource.cueSheet = CueSheet.SE.ToString();
        _criAtomSource.cueName = _cuename;
        _criAtomSource.Play();
    }
}
