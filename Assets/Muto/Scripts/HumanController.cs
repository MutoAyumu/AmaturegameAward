using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : CharacterControllerBase
{
    [SerializeField] PlayerAttack _attack = default;
    [SerializeField] TestMoveTheBlocks _push = default;
    [SerializeField, Tooltip("幽霊が移動するときの指定場所")] Transform _ghostSetPos = default;
    [SerializeField] SpriteRenderer _togetherImage = default;
    [SerializeField, Tooltip("Rayが当たってほしいオブジェクトのレイヤー")] LayerMask _layer = default;
    [Header("入力の時に使うボタンの名前")]
    [SerializeField, Tooltip("攻撃ボタンの名前")] string _attackButtonName = "Fire1";
    [SerializeField, Tooltip("物を掴むボタンの名前")] string _grabButtonName = "Fire1";


    bool _isGrab = false;
    MoveBlock _block = default;

    public Transform GhostSetPos { get => _ghostSetPos;}
    public SpriteRenderer TogetherImage { get => _togetherImage;}
    public bool IsGrab { get => _isGrab; set => _isGrab = value; }

    public override void OnUpdate()
    {
        if(!_isGrab && InputButtonDown(_grabButtonName)) //物を掴むときの処理
        {
            Catch();
        }
        if(_isGrab && Input.GetButton(_grabButtonName)) //物を掴んで動かす時の処理
        {
            MoveIt();
        }   
        else if(_attack && InputButtonDown(_attackButtonName)) //攻撃をするときの処理
        {
            _attack.Attack(_lh, _lv);
        }
        if (_isGrab && InputButtonUp(_grabButtonName)) //物を離す時の処理
        {
            Release();
        }
    }
    /// <summary>
    /// ボタンが押された時に呼ばれる
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
    /// ボタンが押されている時に呼ばれる
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
    /// ボタンが離された時に呼ばれる
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
