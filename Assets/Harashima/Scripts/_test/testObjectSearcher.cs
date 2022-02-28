using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObjectSearcher : MonoBehaviour
{
   [SerializeField,Tooltip("フィールドオブジェクトのレイヤー")]
    LayerMask mask = default;

    /// <summary>
    /// 目の前にオブジェクトがあるかを判定する関数
    /// </summary>
    public void Search(float h,float v)
    {
        //要変更、ここで光のオブジェクトも判定する
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);

        if (hit.collider)
        {
            hit.collider.GetComponent<IAction>().Action();
        }
    }
}
