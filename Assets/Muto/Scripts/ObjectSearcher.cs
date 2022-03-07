using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSearcher : MonoBehaviour
{
    [SerializeField, Tooltip("Ray���������Ăق����I�u�W�F�N�g�̃��C���[")] LayerMask _layer = default;

    public LayerMask Layer { get => _layer;}

    /// <summary>
    /// ���͕����ɃI�u�W�F�N�g�����邩�ǂ���
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    /// <param name="hit"></param>
    public void Search(float h, float v, RaycastHit2D hit)
    {
        if(hit.collider)
        {
            hit.collider.GetComponent<IActivate>()?.Action();
        }
    }
}
