using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        if(CharacterManager.Instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Ghost.transform.position,Quaternion.identity);
        }
        else
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Human.transform.position, Quaternion.identity);
        }
        //����̕����ɓ�����
        Rigidbody2D rb = _instans.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);

        DOVirtual.DelayedCall(1f, () => {
            rb.velocity = Vector2.zero;
        });

        //�������炷
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
