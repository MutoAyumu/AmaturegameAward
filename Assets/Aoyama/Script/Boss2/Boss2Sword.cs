using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Sword : MonoBehaviour
{
    [SerializeField] GameObject _swordObject;
    [SerializeField] Transform _sword1;
    [SerializeField] Transform _sword2;

    public void GenerateSword()
    {
        GameObject.Instantiate(_swordObject, _sword1.position, Quaternion.identity);
        GameObject.Instantiate(_swordObject, _sword2.position, Quaternion.identity);
    }
}
