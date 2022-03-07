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

    GameObject _instans;

    public override void Use()
    {
        //生成する
        if(CharacterManager.Instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Ghost.transform.position,Quaternion.identity);
        }
        else
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Human.transform.position, Quaternion.identity);
        }
        //特定の方向に投げる
        Rigidbody2D rb = _instans.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);

        DOVirtual.DelayedCall(1f, () => {
            rb.velocity = Vector2.zero;
        });

        //数を減らす
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
