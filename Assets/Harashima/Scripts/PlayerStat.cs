using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Singleton<PlayerStat>
{
    /// <summary>
    /// �l���A�C�e�����i�[���郊�X�g
    /// </summary>
    List<GameObject> _inventry = new List<GameObject>();
    public List<GameObject> Inventry => _inventry;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �C���x���g���ɃA�C�e��������֐�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        _inventry.Add(item);
        Debug.Log($"{item}����ɓ��ꂽ");
    }
}
