using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb = default;
    [SerializeField] protected Collider2D _col = default;
    [SerializeField] protected PlayerHP _hp = default;
    [SerializeField] string _endAreaTag = "Finish";
    [SerializeField] protected Animator _anim = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
    [SerializeField] ObjectSearcher _searchar = default;
    [SerializeField, Tooltip("Searcharを呼ぶときのボタンの名前")] string _inputSearchar = "Fire2";
    [SerializeField, Tooltip("Rayの長さ")] protected float _rayLength = 1f;
    [Header("操作キャラのパラメーター"), Space(10)]
    [SerializeField] protected float _moveSpeed = 3.0f;
    [SerializeField] protected CharacterStatus _status = CharacterStatus.IDLE;

    [Tooltip("最後に入力された横方向の値")] protected float _lh = 1;
    public float InputH => _lh;
    [Tooltip("最後に入力された縦方向の値")] protected float _lv = default;
    public float InputV => _lv;

    [Tooltip("どっちのキャラが操作されているかのフラグ")] protected bool _isControll = default;

    [SerializeField, Tooltip("ステージクリアで使うトリガーのタグ")] string _endTag = "Finish";

    protected float _h = default;
    protected float _v = default;
    protected float _currentSpeed;

    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public PlayerHP Hp { get => _hp; }
    public SpriteRenderer MainSprite { get => _mainSprite; }
    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    public Animator Anim { get => _anim; }
    public Collider2D Col { get => _col; }

    protected enum CharacterStatus
    {
        IDLE,
        WALK,
        ATTACK,
        NOCK_BACK,
        ACTION
    }
    private void Start()
    {
        _currentSpeed = _moveSpeed;

        OnStart();
    }
    public virtual void OnStart()
    {

    }
    private void Update()
    {
        if (_isControll)
        {
            InputValue();

            //状態を管理する
            switch (_status)
            {
                case CharacterStatus.IDLE:
                    _anim.SetBool("IsMove", false);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, hit);
                    }
                    break;

                case CharacterStatus.WALK:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, hit);
                    }
                    break;

                case CharacterStatus.ATTACK:
                    _rb.velocity = Vector2.zero;
                    break;

                case CharacterStatus.NOCK_BACK:
                    break;

                case CharacterStatus.ACTION:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);
                    break;

                default:
                    break;
            }

            OnUpdate();
        }
    }
    /// <summary>
    /// 派生先で使うUpdate
    /// </summary>
    public virtual void OnUpdate()
    {

    }
    /// <summary>
    /// 移動入力を受ける関数
    /// </summary>
    protected void InputValue()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        if (_status != CharacterStatus.ATTACK && _status != CharacterStatus.ACTION)
        {
            if (_h == 0 && _v == 0)
            {
                _status = CharacterStatus.IDLE;
            }
            else
            {
                _status = CharacterStatus.WALK;
            }
        }

        if (_status != CharacterStatus.ACTION)
        {
            if (_h != 0 || _v != 0)
            {
                if (_lh != _h || _lv != _v)
                {
                    _lh = _h;
                    _lv = _v;
                }
            }
        }

        if (_anim)
        {
            _anim.SetFloat("X", _lh);
            _anim.SetFloat("Y", _lv);
        }
    }
    /// <summary>
    /// 操作キャラを動かす関数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    protected void Move(float h, float v)
    {
        if (!TutorialManager.Instance.CutSceneFlag)
        {
            var dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _currentSpeed;

            Debug.DrawRay(this.transform.position, new Vector2(_lh, _lv).normalized * _rayLength, Color.red);
        }
    }
    /// <summary>
    /// 操作キャラを止める関数
    /// </summary>
    public void Stop()
    {
        if (_rb)
        {
            _rb.velocity = Vector2.zero;
            _anim.SetBool("IsMove", false);
        }
        else
        {
            Debug.LogError("Rigidbody2Dがありません");
        }
    }
    /// <summary>
    /// 最後に向いている方向を返す関数
    /// </summary>
    /// <returns></returns>
    public Vector2 ReturnTheDirection()
    {
        return new Vector2(_lh, _lv);
    }

    public Vector2 ColliderCenter()
    {
        return (Vector2)this.transform.position + _col.offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_endTag))
        {
            FieldManager.Instance.Clear();
        }
    }
}
