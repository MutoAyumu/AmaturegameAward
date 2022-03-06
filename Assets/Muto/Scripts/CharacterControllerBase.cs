using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb = default;
    [SerializeField] string _endAreaTag = "Finish";
    [SerializeField] Animator _anim = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
    [Header("操作キャラのパラメーター"), Space(10)]
    [SerializeField] protected float _moveSpeed = 3.0f;

    [Tooltip("最後に入力された横方向の値")]protected float _h = default;
    [Tooltip("最後に入力された縦方向の値")] protected float _v = default;
    [Tooltip("どっちのキャラが操作されているかのフラグ")] protected bool _isControll = default;

    public bool IsControll { get => _isControll; set => _isControll = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public SpriteRenderer MainSprite { get => _mainSprite;}

    private void Update()
    {
        if(_isControll)
        {
            Move(InputValueH(), InputValueV());
        }

        OnUpdate();
    }
    /// <summary>
    /// 派生先で使うUpdate
    /// </summary>
    public virtual void OnUpdate()
    {
        
    }
    /// <summary>
    /// 横移動入力を受ける関数
    /// </summary>
    protected float InputValueH()
    {
        var h = Input.GetAxisRaw("Horizontal");

        if(h != 0)//入力された値を保存しておく
        {
            _h = h;
        }

        return h;
    }
    /// <summary>
    /// 縦移動入力を受ける関数
    /// </summary>
    /// <returns></returns>
    protected float InputValueV()
    {
        var v = Input.GetAxisRaw("Vertical");

        if (v != 0)//入力された値を保存しておく
        {
            _v = v;
        }

        return v;
    }
    /// <summary>
    /// ボタンが押されたかを返す関数
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected bool InputButtonDown(string button)
    {
        if(Input.GetButtonDown(button))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// ボタンが押されているかを返す関数
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    protected bool InputButton(string button)
    {
        if (Input.GetButtonDown(button))
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// 操作キャラを動かす関数
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    protected void Move(float h, float v)
    {
        var dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _moveSpeed;
    }
    /// <summary>
    /// 操作キャラを止める関数
    /// </summary>
    public void Stop()
    {
        if(_rb)
        {
            _rb.Sleep();
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
        return new Vector2(_h, _v);
    }
}
