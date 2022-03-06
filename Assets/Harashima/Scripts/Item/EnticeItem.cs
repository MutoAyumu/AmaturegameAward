using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G��U���A�C�e���̃N���X
/// </summary>
public class EnticeItem : ItemBase
{
    [SerializeField, Tooltip("�G��U���A�C�e���̃v���n�u")]
    GameObject _instansItem;

    public override void Use()
    {
        if(CharacterManager._instance.Ghost.IsControll)
        {
            GameObject go = Instantiate(_instansItem, CharacterManager._instance.Ghost.transform.position,Quaternion.identity);
            //Rigidbody2D rb = go.GetComponent<Rigidbody2D>().AddForce()
        }
        else
        {
            GameObject go = Instantiate(_instansItem, CharacterManager._instance.Human.transform.position, Quaternion.identity);
        }
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
