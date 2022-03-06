using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemBase : MonoBehaviour
{
    [SerializeField,Tooltip("アイテムのクールタイム")]
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
    /// 今はBaseで入力を受け取っているがManagerクラスで受け取る様に変更
    /// </summary>
    void Use()
    {
        _timer += Time.deltaTime;
        if(_timer>= _coolTime && Input.GetKeyDown(_valueKey))
        {
            Debug.Log($"{this.gameObject.name}が使用されました");
            _timer = 0f;
        }
        else if(Input.GetKeyDown(_valueKey))
        {
            Debug.Log($"{this.gameObject.name}はクールタイム中です");
        }
    }
}
