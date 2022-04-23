using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float _time = 1;

    void Start()
    {
        Destroy(gameObject, _time);
    }
}
