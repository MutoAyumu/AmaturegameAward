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

    [SerializeField, Tooltip("�����Ă���Î~����܂ł̎���")]
    float _stopTime = 1f;

    [SerializeField, Tooltip("������܂ł̎���")]
    float _destroyTime = 3f;

    GameObject _instans;

    public override void Use()
    {
        Rigidbody2D rb ;
        //��������
        if (CharacterManager.Instance.Ghost.IsControll)
        {
            _instans = Instantiate(_instansItem, CharacterManager.Instance.Ghost.transform.position,Quaternion.identity);
            //����̕����ɓ�����
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

        //�������炷
        ItemManager.Instance.ItemValueChange(_itemID, -1);
    }
}
