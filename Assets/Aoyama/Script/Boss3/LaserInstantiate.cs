using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInstantiate : MonoBehaviour
{
    [SerializeField] Transform _muzzle;
    [SerializeField] GameObject go;
    public void Instantiate()
    {
        Instantiate(go, _muzzle.position, Quaternion.identity, transform);
    }
}
