using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GhostController : CharacterControllerBase
{
    [SerializeField, Tooltip("Ray���������Ăق����I�u�W�F�N�g�̃��C���[")] LayerMask _layer = default;
    [SerializeField, Tooltip("������鎞�Ɏg���{�^���̖��O")] string _inputLight = "Fire2";
    [SerializeField] LightAbsorption _abs = default;

    bool _isFixedRange = default;

    public bool IsFixedRange { get => _isFixedRange; set => _isFixedRange = value; }

    public override void OnUpdate()
    {
        if(Input.GetButtonDown(_inputLight) && _isControll)
        {
            //Activate();
            _abs.Absorption(_lh, _lv, _rayLength, _layer);
        }
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
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLength, _layer);

        if(hit.collider)
        {
            hit.collider.GetComponent<IGhostGimic>()?.Action();
        }
    }
}
