using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSwitch : MonoBehaviour, IActivate
{
    [SerializeField,Tooltip("�X�C�b�`�ɑΉ�����")]
    GameObject[] _obstacleObejects;
    bool IsActive;
    [SerializeField] Sprite _activeSprite = default;
    [SerializeField] Sprite _inactiveSprite = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
    [SerializeField] Transform _camTarget = default;
    public void Action()
    {
        foreach(var i in _obstacleObejects)
        {
            i.GetComponent<IActivate>()?.Action();
        }

        if (!IsActive)
        {
            _mainSprite.sprite = _activeSprite;
            IsActive = true;
        }
        else
        {
            _mainSprite.sprite = _inactiveSprite;
            IsActive = false;
        }
        Debug.Log("�쓮");
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, "ObjectSwitch");

        if(_camTarget)
        FieldManager.Instance.SetEventCamera(_camTarget);
    }
}
