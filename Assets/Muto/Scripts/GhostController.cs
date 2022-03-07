using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GhostController : CharacterControllerBase
{
    [SerializeField, Tooltip("Ray���������Ăق����I�u�W�F�N�g�̃��C���[")] LayerMask _layer = default;
    [SerializeField, Tooltip("�H�삪�ێ����邱�Ƃ��o��������̂���Ղ���")] int _upperLimit = 3;
    [SerializeField, Tooltip("������鎞�Ɏg���{�^���̖��O")] string _inputLight = "Fire2";
    [SerializeField,Tooltip("�H��ɃA�^�b�`����Ă���Light2D������")] Light2D _light = default;
    bool _isFixedRange = default;
    int _lightCount = 0;

    public bool IsFixedRange { get => _isFixedRange; set => _isFixedRange = value; }
    public Light2D Light { get => _light;}
    public int UpperLimit { get => _upperLimit;}
    public int LightCount { get => _lightCount; set => _lightCount = value; }

    public override void OnUpdate()
    {
        if(InputButtonDown(_inputLight) && _isControll)
        {
            Activate();
        }

        Debug.DrawRay(this.transform.position, new Vector2(_lh, _lv).normalized * _rayLenght, Color.red);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !_isFixedRange)
        {
            _isFixedRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isFixedRange = false;
        }
    }
    /// <summary>
    /// �H��ɂ��������鏈���̊֐�
    /// </summary>
    void Activate()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLenght, _layer);
        //var light = hit.collider?.GetComponent<LightSource>();

        //if(light && light.IsOn && _lightCount < _upperLimit)
        //{
        //    Stop();
        //    _lightCount++;
        //    light.Switching();
        //}
        //else if(light && light.IsOn && _lightCount > _upperLimit)
        //{
        //    Stop();
        //    _lightCount--;
        //    light.Switching();
        //}

        if(hit.collider)
        {
            hit.collider.GetComponent<IGhostGimic>()?.Action();
        }
    }
}
