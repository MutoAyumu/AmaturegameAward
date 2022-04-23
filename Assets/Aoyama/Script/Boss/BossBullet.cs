using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _muzzle;

    public void Instantiate()
    {
        Instantiate(_bullet, _muzzle.position, Quaternion.identity);
    }
}
