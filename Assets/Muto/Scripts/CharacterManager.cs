using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField, Tooltip("Humanプレパブを入れる")] HumanController _human = default;
    [SerializeField, Tooltip("Ghostプレパブを入れる")] GhostController _ghost = default;
    [SerializeField, Tooltip("要素0が人間　要素1が幽霊")] Transform[] _instancePos = new Transform[2];
    [SerializeField, Tooltip("Vcamを入れる")] ICinemachineCamera _vcam = default;
    [SerializeField] Text _lightCountTest = default;
    [SerializeField, Tooltip("操作キャラを人間に変更するボタンの名前")] string _humanChangeButton = "Jump";
    [SerializeField, Tooltip("操作キャラを幽霊に変更するボタンの名前")] string _ghostChangeButton = "Jump";

    public HumanController Human { get => _human;}
    public GhostController Ghost { get => _ghost;}
    public ICinemachineCamera Vcam { get => _vcam; set => _vcam = value; }
    public Text LightCountTest { get => _lightCountTest;}

    private void Start()
    {
        StartInstantiate();
    }
    private void Update()
    {
        if(Input.GetButtonDown(_humanChangeButton) || Input.GetButtonDown(_ghostChangeButton))
        {
            Exchange();
        }
    }
    /// <summary>
    /// ゲームを始める準備
    /// </summary>
    void StartInstantiate()
    {
        _human = Instantiate(_human, _instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        _vcam.Follow = _human.transform;
        _human.IsControll = true;
    }
    /// <summary>
    /// 操作キャラを切り替える関数
    /// </summary>
    void Exchange()
    {
        _ghost.Together = false;

        if(_human.IsControll)   //人間を操作している
        {
            _human.IsControll = false;
            _ghost.IsControll = true;

            _vcam.Follow = _ghost.transform;
        }
        else　　//幽霊を操作している
        {
            _ghost.IsControll = false;
            _human.IsControll = true;

            _vcam.Follow = _human.transform;
        }
    }
}
