using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheLightSource : MonoBehaviour
{
    [SerializeField, Tooltip("フィールドオブジェクトのレイヤー")]
    LayerMask mask = default;

    /// <summary>
    /// 幽霊が目の前にある光源を吸収するための関数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void Take(float h, float v)
    {
        //要変更、ここで光のオブジェクトも判定する
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);

        if (hit.collider)
        {
            hit.collider.GetComponent<MovingLightSource>().IsMoving();
        }
    }
}
