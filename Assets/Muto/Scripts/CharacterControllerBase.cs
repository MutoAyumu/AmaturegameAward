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
    [SerializeField] protected float _currentSpeed;

    [SerializeField] protected MessageCharacter _message = default;

    [SerializeField] protected CriAtomSource _atomSource = default;

    FieldManager _fieldManager;

    bool IsPause;
    protected bool IsSetText;

    RaycastHit2D _hit;

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
        DAMAGE,
        ACTION,
        Dead
    }

    private void OnEnable()
    {
        _fieldManager.OnPause += Pause;
        _fieldManager.OnResume += Resume;
    }
    private void OnDisable()
    {
        _fieldManager.OnPause -= Pause;
        _fieldManager.OnResume -= Resume;
    }
    private void Awake()
    {
        _fieldManager = FieldManager.Instance;
        OnAwake();
    }
    public virtual void OnAwake()
    {

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
        if (_status == CharacterStatus.Dead)
        {
            return;
        }

        if (_isControll && !IsPause)
        {
            InputValue();

            //状態を管理する
            switch (_status)
            {
                case CharacterStatus.IDLE:
                    _anim.SetBool("IsMove", false);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, _hit);
                    }
                    break;

                case CharacterStatus.WALK:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);

                    if (Input.GetButtonDown(_inputSearchar) && _searchar)
                    {
                        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);
                        _searchar.Search(_lh, _lv, _hit);
                    }
                    break;

                case CharacterStatus.ATTACK:
                    _rb.velocity = Vector2.zero;
                    break;

                case CharacterStatus.DAMAGE:
                    break;

                case CharacterStatus.ACTION:
                    Move(_h, _v);
                    _anim.SetBool("IsMove", true);
                    break;

                case CharacterStatus.Dead:
                    _anim.SetBool("IsDead", true);
                    break;

                default:
                    break;
            }

            OnUpdate();
        }
        else if (!IsControll)// && _interactImage.activeSelf)
        {
            //_interactImage.SetActive(false);
        }
    }
    /// <summary>
    /// 派生先で使うUpdate
    /// </summary>
    public virtual void OnUpdate()
    {

    }
    protected virtual void Interact()
    {
        _hit = Physics2D.Raycast(this.transform.position, new Vector2(_lh, _lv), _rayLength, _searchar.Layer);

        if (_hit && !IsSetText)
        {
            IsSetText = true;
            CharacterManager.Instance.SetIntaractText(_hit.collider?.GetComponent<ISetText>().SetText());
        }
        else if (!_hit && IsSetText)
        {
            IsSetText = false;
            CharacterManager.Instance.ClearIntaractText();
        }
    }
    /// <summary>
    /// 移動入力を受ける関数
    /// </summary>
    protected void InputValue()
    {
        if (_status == CharacterStatus.DAMAGE) return;

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

                Interact();
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
        if (TimeLineManager.Instance)
        {
            if (TimeLineManager.Instance.CutSceneFlag)
                return;
        }

        var dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _currentSpeed;

        Debug.DrawRay(this.transform.position, new Vector2(_lh, _lv).normalized * _rayLength, Color.red);
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(_endTag))
    //    {
    //        FieldManager.Instance.Clear();
    //    }
    //}
    public void Pause()
    {
        _rb.Sleep();
        _anim.speed = 0;
        IsPause = true;
    }
    public void Resume()
    {
        _rb.WakeUp();
        _anim.speed = 1;
        IsPause = false;
    }
    public void ChangerMessageFlag(bool flag)
    {
        if (_message)
        {
            _message.IsMessage(flag);
        }
    }
    public void IsDead()
    {
        _status = CharacterStatus.Dead;
    }
    public virtual void IsDamage()
    {
        _rb.AddForce(-new Vector2(_lh, _lv).normalized * 20, ForceMode2D.Impulse);
        StartCoroutine(DamageStart());
        _status = CharacterStatus.DAMAGE;
    }
    IEnumerator DamageStart()
    {
        yield return new WaitForSeconds(1.5f);
        _status = CharacterStatus.IDLE;
    }
}
