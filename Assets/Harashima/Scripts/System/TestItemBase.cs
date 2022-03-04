using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemBase : MonoBehaviour
{
    [SerializeField,Tooltip("�A�C�e���̃N�[���^�C��")]
    float _coolTime = 5f;

    float _timer = 0f;

    [SerializeField]
    KeyCode _valueKey;


    private void Start()
    {
        _timer = _coolTime;
    }
    void Update()
    {
        Use();
    }

    /// <summary>
    /// ����Base�œ��͂��󂯎���Ă��邪Manager�N���X�Ŏ󂯎��l�ɕύX
    /// </summary>
    void Use()
    {
        _timer += Time.deltaTime;
        if(_timer>= _coolTime && Input.GetKeyDown(_valueKey))
        {
            Debug.Log($"{this.gameObject.name}���g�p����܂���");
            _timer = 0f;
        }
        else if(Input.GetKeyDown(_valueKey))
        {
            Debug.Log($"{this.gameObject.name}�̓N�[���^�C�����ł�");
        }
    }
}
