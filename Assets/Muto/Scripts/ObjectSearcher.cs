using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSearcher : MonoBehaviour
{
    [SerializeField, Tooltip("Rayが当たってほしいオブジェクトのレイヤー")] LayerMask _layer = default;

    public LayerMask Layer { get => _layer;}

    /// <summary>
    /// 入力方向にオブジェクトがあるかどうか
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
