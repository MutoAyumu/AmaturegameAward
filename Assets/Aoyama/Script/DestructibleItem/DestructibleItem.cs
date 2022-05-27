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

    [SerializeField, Tooltip("確率でドロップするアイテム")]
    GameObject _dropObject = default;

    [SerializeField, Tooltip("ドロップする確率の分母")]
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

        Debug.Log($"{gameObject.name}を壊した");

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
