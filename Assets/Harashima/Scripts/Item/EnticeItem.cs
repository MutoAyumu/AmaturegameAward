using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 敵を誘うアイテムのクラス
/// </summary>
public class EnticeItem : ItemBase
{
    [SerializeField, Tooltip("敵を誘うアイテムのプレハブ")]
    GameObject _instansItem;

    [SerializeField, Tooltip("投げてから静止するまでの時間")]
    float _stopTime = 1f;

    [SerializeField, Tooltip("消えるまでの時間")]
    float _destroyTime = 3f;

    GameObject _instans;

    public override void Use()
    {
        Rigidbody2D rb ;
        //生成する
        if (CharacterManager.Instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Ghost.transform.position,Quaternion.identity);
            //特定の方向に投げる
            rb = _instans.GetComponent<Rigidbody2D>();
            //rb.AddForce(new Vector2(CharacterManager.Instance.Ghost._h, CharacterManager.Instance.Ghost._v), ForceMode2D.Impulse);
        }
        else
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Human.transform.position, Quaternion.identity);
            rb = _instans.GetComponent<Rigidbody2D>();
            //rb.AddForce(new Vector2(CharacterManager.Instance.Human._h, CharacterManager.Instance.Human._v), ForceMode2D.Impulse);
        }
       

        DOVirtual.DelayedCall(_stopTime, () => {
            rb.velocity = Vector2.zero;
            Destroy(_instans,_destroyTime);
        });

        //数を減らす
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
