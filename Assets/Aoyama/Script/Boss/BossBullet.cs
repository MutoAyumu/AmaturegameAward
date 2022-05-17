using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _muzzle;
    [Header("‰¹")]
    [SerializeField] CriAtomSource _criAtomSource;
    [SerializeField] string _cuename = "GaBossTama";

    public void Instantiate()
    {
        _criAtomSource.cueSheet = CueSheet.SE.ToString();
        _criAtomSource.cueName = _cuename;
        _criAtomSource.Play();
        Instantiate(_bullet, _muzzle.position, Quaternion.identity);
    }

}
