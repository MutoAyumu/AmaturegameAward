using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class MovingLightSource : MonoBehaviour
{
    [SerializeField] Light2D _light = default;
    [SerializeField] float _time = 0.5f;
    [SerializeField] bool _isOn = true;
    [SerializeField, Tooltip("IActivate���p������GameObject������")] GameObject[] _activate = default;

    private void Start()//��Œ���
    {
        IsStartFalse();
    }
    void IsStartFalse()
    {
        if (_isOn)
        {
            _light.intensity = 1;
        }
        else
        {
            _light.intensity = 0;
        }
    }
    /// <summary>
    /// ���C�g�̃I���I�t������֐�
    /// </summary>
    public void IsMoving()
    {
        if (_light)
        {
            if (_isOn)   //����������ꍇ
            {
                DOVirtual.Float(_light.intensity, 0, _time, value => _light.intensity = value)  //dotween��intensity���P����O�ɂ��Ă���
                    .OnComplete(() =>
                    {
                        _isOn = false;
                        CharacterManager._instance.Ghost._lightNum++;
                    });
            }
            else
            {
                DOVirtual.Float(_light.intensity, 1, _time, value => _light.intensity = value)
                    .OnComplete(() =>
                    {
                        _isOn = true;
                        CharacterManager._instance.Ghost._lightNum--;

                        foreach (var go in _activate)
                        {
                            go.GetComponent<IActivate>()?.Action();
                        }
                    });
            }
        }
        else
        {
            Debug.LogError("2DLight���Z�b�g����Ă��܂���");
        }
    }
}
