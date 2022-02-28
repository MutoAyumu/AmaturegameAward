using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance = default;
    [SerializeField] HumanController _player = default;
    [SerializeField] GhostController _ghost = default;
    [SerializeField] string _inputButton = "Jump";
    [SerializeField] CinemachineVirtualCamera _vcam = default;
    [SerializeField, Tooltip("要素0が人間　要素1が幽霊")] Transform[] _instancePos = new Transform[2];

    public HumanController Player { get => _player; }
    public GhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }

    private void Awake()
    {
        if(_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            FieldManager.Instance.OnStart += StartInstantiate;
        }
    }
    public void StartInstantiate()
    {
        _player = Instantiate(_player,_instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

        _vcam.Follow = _player.transform;
        _player.IsControll = true;
    }
    private void Update()
    {
        //元に戻る
        if(Input.GetButtonDown(_inputButton))
        {
            Changed();
        }
    }
    /// <summary>
    /// 操作キャラの切り替え用
    /// </summary>
    void Changed()
    {
        if(_player.IsControll)  //プレイヤーが動いている
        {
            _player.IsControll = false;
            _ghost.IsControll = true;

            _ghost.IsFollow = false;

            _ghost.Rb.constraints = RigidbodyConstraints2D.None;
            _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _player.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

            //カメラを切り替える
            _vcam.Follow = _ghost.transform;
        }
        else   //ゴーストが動いている
        {
            _ghost.IsControll = false;
            _player.IsControll = true;

            _player.Rb.constraints = RigidbodyConstraints2D.None;
            _player.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

            _vcam.Follow = _player.transform;
        }
    }

}
