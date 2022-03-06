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

    GameObject _instans;

    public override void Use()
    {
        //��������
        if(CharacterManager._instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager._instance.Ghost.transform.position,Quaternion.identity);
        }
        else
        {
            _instans = Instantiate(_instansItem, CharacterManager._instance.Human.transform.position, Quaternion.identity);
        }
        //����̕����ɓ�����
        Rigidbody2D rb = _instans.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);

        //�������炷
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
