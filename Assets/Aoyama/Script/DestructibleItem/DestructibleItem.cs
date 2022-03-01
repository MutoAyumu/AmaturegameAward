using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 破壊可能なアイテムのクラス
/// </summary>
public class DestructibleItem : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("何回攻撃したら壊れるか")]
    int _hp = 1;
    [SerializeField, Tooltip("壊したときに生成するPrefab")]
    GameObject _breakObject;

    public void Damage()
    {
        if(--_hp != 0)
        {
            return;
        }

        Debug.Log($"{gameObject.name}を壊した");

        Destroy(gameObject);
        if(_breakObject)
        {
            var go = Instantiate(_breakObject, transform.position, Quaternion.identity);
        }
    }
}
