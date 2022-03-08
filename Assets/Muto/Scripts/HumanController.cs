using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("�H�삪�ړ�����Ƃ��̎w��ꏊ")] Transform _ghostSetPos = default;
    [SerializeField] SpriteRenderer _togetherImage = default;
    [SerializeField, Tooltip("Ray���������Ăق����I�u�W�F�N�g�̃��C���[")] LayerMask _layer = default;
    [Header("���͂̎��Ɏg���{�^���̖��O")]
    [SerializeField, Tooltip("�U���{�^���̖��O")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("����͂ރ{�^���̖��O")] string _grabButtonName = "Fire1";


    bool _isGrab = false;
    MoveBlock _block = default;

    public Transform GhostSetPos { get => _ghostSetPos;}
    public SpriteRenderer TogetherImage { get => _togetherImage;}
    public bool IsGrab { get => _isGrab; set => _isGrab = value; }

    public override void OnUpdate()
    {
        if(!_isGrab && InputButtonDown(_grabButtonName)) //����͂ނƂ��̏���
        {
            Catch();
        }
        if(_isGrab && Input.GetButton(_grabButtonName)) //����͂�œ��������̏���
        {
            MoveIt();
        }   
        else if(_attack && InputButtonDown(_attackButtonName)) //�U��������Ƃ��̏���
        {
            _attack.Attack(_lh, _lv);
        }
        if (_isGrab && InputButtonUp(_grabButtonName)) //���𗣂����̏���
        {
            Release();
        }
    }
    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// </summary>
    void Catch()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv), _rayLength, _layer);

        if(hit.collider)
        {
            _block = hit.collider.GetComponent<MoveBlock>();
            _block.Rb.bodyType = RigidbodyType2D.Dynamic;
            _isGrab = true;
            Debug.Log("Catch");
        }
    }
    /// <summary>
    /// �{�^����������Ă��鎞�ɌĂ΂��
    /// </summary>
    void MoveIt()
    {
        if(_block)
        {
            _block.Rb.velocity = new Vector2(_h, _v).normalized * _moveSpeed;
            Debug.Log("Move");
        }
    }
    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// </summary>
    void Release()
    {
        if(_block)
        {
            _block.Rb.velocity = Vector2.zero;
            _block.Rb.bodyType = RigidbodyType2D.Kinematic;
            _block = null;
            _isGrab = false;
            Debug.Log("Release");
        }
    }
    void Activate()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(_lh, _lv).normalized, _rayLength, _layer);

        if(hit.collider)
        {
            hit.collider.GetComponent<IHumanGimic>()?.Action();
        }
    }
}
