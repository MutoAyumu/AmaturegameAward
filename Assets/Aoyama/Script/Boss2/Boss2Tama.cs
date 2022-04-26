using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Tama : MonoBehaviour
{
    [SerializeField] Transform _muzzle;
    [SerializeField] GameObject _tama;

    public void GenerateTama()
    {
        GameObject.Instantiate(_tama, transform.position, Quaternion.identity);
    }
}
