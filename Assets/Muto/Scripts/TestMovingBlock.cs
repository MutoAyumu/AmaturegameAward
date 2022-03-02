using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingBlock : MonoBehaviour
{
    [SerializeField] string _tags = " ";
    [SerializeField] Rigidbody2D _rb = default;
    [SerializeField, Tooltip("IActivate���p������GameObject������")] GameObject[] _activate = default;

    public Rigidbody2D Rb { get => _rb;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_tags))
        {
            Debug.Log("�ړI�n�ɓ���");

            foreach(var go in _activate)
            {
                go.GetComponent<IActivate>()?.Action();
            }
        }
    }
}
