using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �j��\�ȃA�C�e���̃N���X
/// </summary>
public class DestructibleItem : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("����U����������邩")]
    int _hp = 1;
    [SerializeField, Tooltip("�󂵂��Ƃ��ɐ�������Prefab")]
    GameObject _breakObject;

    public void Damage()
    {
        if(--_hp != 0)
        {
            return;
        }

        Debug.Log($"{gameObject.name}���󂵂�");

        Destroy(gameObject);
        if(_breakObject)
        {
            var go = Instantiate(_breakObject, transform.position, Quaternion.identity);
        }
    }
}
