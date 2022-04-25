using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSwitch : MonoBehaviour, IActivate
{
    [SerializeField,Tooltip("スイッチに対応する")]
    GameObject[] _obstacleObejects;
    bool IsActive;
    [SerializeField] Sprite _activeSprite = default;
    [SerializeField] Sprite _inactiveSprite = default;
    [SerializeField] SpriteRenderer _mainSprite = default;
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
        Debug.Log("作動");
    }
}
