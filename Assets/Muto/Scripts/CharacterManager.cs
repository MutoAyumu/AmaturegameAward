using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField, Tooltip("Human�v���p�u������")] HumanController _human = default;
    [SerializeField, Tooltip("Ghost�v���p�u������")] GhostController _ghost = default;
    [SerializeField, Tooltip("�v�f0���l�ԁ@�v�f1���H��")] Transform[] _instancePos = new Transform[2];
    [SerializeField, Tooltip("Vcam������")] ICinemachineCamera _vcam = default;
    [SerializeField] Text _lightCountTest = default;
    [SerializeField, Tooltip("����L������l�ԂɕύX����{�^���̖��O")] string _humanChangeButton = "Jump";
    [SerializeField, Tooltip("����L������H��ɕύX����{�^���̖��O")] string _ghostChangeButton = "Jump";

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
    /// �Q�[�����n�߂鏀��
    /// </summary>
    void StartInstantiate()
    {
        _human = Instantiate(_human, _instancePos[0].position, Quaternion.identity);
        _ghost = Instantiate(_ghost, _instancePos[1].position, Quaternion.identity);

        _vcam.Follow = _human.transform;
        _human.IsControll = true;
    }
    /// <summary>
    /// ����L������؂�ւ���֐�
    /// </summary>
    void Exchange()
    {
        _ghost.Together = false;

        if(_human.IsControll)   //�l�Ԃ𑀍삵�Ă���
        {
            _human.IsControll = false;
            _ghost.IsControll = true;

            _vcam.Follow = _ghost.transform;
        }
        else�@�@//�H��𑀍삵�Ă���
        {
            _ghost.IsControll = false;
            _human.IsControll = true;

            _vcam.Follow = _human.transform;
        }
    }
}
