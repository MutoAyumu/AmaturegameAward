using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(CharacterManager._instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager._instance.Ghost.transform.position,Quaternion.identity);
        }
        else
        {
            _instans = Instantiate(_instansItem, CharacterManager._instance.Human.transform.position, Quaternion.identity);
        }
        //特定の方向に投げる
        Rigidbody2D rb = _instans.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);

        //数を減らす
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
