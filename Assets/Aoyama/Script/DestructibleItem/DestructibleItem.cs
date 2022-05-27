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

    [SerializeField, Tooltip("�m���Ńh���b�v����A�C�e��")]
    GameObject _dropObject = default;

    [SerializeField, Tooltip("�h���b�v����m���̕���")]
    int _dropProbability = 5;

    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;

    [SerializeField] string _cueName = "";

    public void Damage(int damage)
    {
        if (_audio)
        {
            SoundManager.Instance.SoundPlay(_audio);
        }
        if (--_hp != 0)
        {
            return;
        }

        Debug.Log($"{gameObject.name}���󂵂�");

        if(_cueName != "")
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);

        if (_breakObject)
        {
            var go = Instantiate(_breakObject, transform.position, Quaternion.identity);
        }
        if(_dropObject)
        {
            int luck = Random.Range(0,_dropProbability);
            if(luck ==0)
            {
                Instantiate(_dropObject, transform.position, Quaternion.identity);
            }
            
        }
        Destroy(gameObject);

    }
}
