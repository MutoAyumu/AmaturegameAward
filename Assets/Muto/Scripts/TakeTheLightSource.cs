using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheLightSource : MonoBehaviour
{
    [SerializeField, Tooltip("フィールドオブジェクトのレイヤー")]
    LayerMask mask = default;
    [SerializeField, Tooltip("保持できる光源の上限")] int _upperLimit = 5;

    /// <summary>
    /// 幽霊が目の前にある光源を吸収したり戻したりするための関数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void delivery(float h, float v)
    {
        //要変更、ここで光のオブジェクトも判定する
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);
        var light = hit.collider.GetComponent<MovingLightSource>();

        if (hit.collider && light.IsOn && CharacterManager._instance.Ghost._lightNum < _upperLimit)
        {
            CharacterManager._instance.Ghost.Stop();
            light.IsMoving();
        }
        else if(hit.collider && !light.IsOn && CharacterManager._instance.Ghost._lightNum > 0)
        {
            CharacterManager._instance.Ghost.Stop();
            light.IsMoving();
        }
    }
}
