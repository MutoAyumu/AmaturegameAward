using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager _instance = default;

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

    [SerializeField] CharacterControllerBase _player = default;
    [SerializeField] CharacterControllerBase _ghost = default;
    [SerializeField] string _inputButton = "Jump";
    [SerializeField] CinemachineVirtualCamera _vcam = default;

    public CharacterControllerBase Player { get => _player;}
    public CharacterControllerBase Ghost { get => _ghost;}

    private void Start()
    {
        _player = Instantiate(_player);
        _ghost = Instantiate(_ghost);

        _vcam.Follow = _player.transform;
    }
    private void Update()
    {
        //���ɖ߂�
        if(Input.GetButtonDown(_inputButton))
        {
            Changed();
        }
    }
    /// <summary>
    /// ����L�����̐؂�ւ��p
    /// </summary>
    void Changed()
    {
        if(_player.IsControll)  //�v���C���[�������Ă���
        {
            _player.IsControll = false;
            _ghost.IsControll = true;

            _player.Rb.bodyType = RigidbodyType2D.Kinematic;

            //�J������؂�ւ���
            _vcam.Follow = _ghost.transform;
        }
        else   //�S�[�X�g�������Ă���
        {
            _ghost.IsControll = false;
            _player.IsControll = true;

            _ghost.Rb.bodyType = RigidbodyType2D.Kinematic;

            _vcam.Follow = _player.transform;
        }
    }

}
