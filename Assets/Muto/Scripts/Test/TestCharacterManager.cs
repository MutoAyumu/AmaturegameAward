using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class TestCharacterManager : MonoBehaviour
{
    public static TestCharacterManager _instance = default;
    [SerializeField] TestHumanController _human = default;
    [SerializeField] TestGhostController _ghost = default;
    [SerializeField] string _inputButton = "Jump";
    [SerializeField] CinemachineVirtualCamera _vcam = default;
    [SerializeField, Tooltip("要素0が人間　要素1が幽霊")] Transform[] _instancePos = new Transform[2];
    [SerializeField] Text _lightCountText = default;

    public TestHumanController Human { get => _human; }
    public TestGhostController Ghost { get => _ghost; }
    public CinemachineVirtualCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountText { get => _lightCountText;}

    /*ToDo
        RigidbodyConstraints2DじゃなくてSleepとかにするかも
        今どっちを操作しているかを変数にしておく
    */
    private void Awake()
    {
        if(_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;            
        }
    }

    private void Start()
    {
        StartInstantiate();
    }
    public void StartInstantiate()
    {
        _human = Instantiate(_human,_instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

        _vcam.Follow = _human.transform;
        _human.IsControll = true;
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
        if(_human.IsControll)  //プレイヤーが動いている
        {
            _human.IsControll = false;
            _ghost.IsControll = true;

            _ghost.IsFollow = false;

            _ghost.Rb.constraints = RigidbodyConstraints2D.None;
            _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _human.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

            //カメラを切り替える
            _vcam.Follow = _ghost.transform;
        }
        else   //ゴーストが動いている
        {
            _ghost.IsControll = false;
            _human.IsControll = true;

            _human.Rb.constraints = RigidbodyConstraints2D.None;
            _human.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _ghost.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

            _vcam.Follow = _human.transform;
        }
    }

}
