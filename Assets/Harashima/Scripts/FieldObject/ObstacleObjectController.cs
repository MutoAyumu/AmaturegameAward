using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour, IActivate
{
    [SerializeField, Tooltip("åªç›çÏìÆÇµÇƒÇ¢ÇÈÇ©")]
    bool isActive = false;

    [SerializeField, Tooltip("çÏìÆÇµÇΩéûÇ…ï\é¶Ç≥ÇπÇÈÇ‡ÇÃ")]
    GameObject[] _activeObstacleObject;

    [SerializeField, Tooltip("çÏìÆÇµÇƒÇ¢Ç»Ç¢éûÇ…ï\é¶Ç≥ÇπÇÈÇ‡ÇÃ")]
    GameObject[] _inactiveObstacleObject;

    [SerializeField] CriAtomSource _criAtom = default;

    private void Start()
    {
        if(isActive)    //çÏìÆ
        {
            foreach(var go in _inactiveObstacleObject)
            {
                go.gameObject?.SetActive(!isActive);
            }

            foreach (var go in _activeObstacleObject)
            {
                go.gameObject?.SetActive(isActive);
            }
        }
        else
        {
            foreach (var go in _activeObstacleObject)
            {
                go.gameObject?.SetActive(isActive);
            }

            foreach (var go in _inactiveObstacleObject)
            {
                go.gameObject?.SetActive(!isActive);
            }
        }
        //_obstacleObject.gameObject?.SetActive(isActive);
    }

    public void Action()
    {
        isActive = !isActive;
        SoundManager.Instance.CriAtomPlay(_criAtom ,CueSheet.SE, "DoorOpen");

        if (isActive)    //çÏìÆ
        {
            foreach (var go in _inactiveObstacleObject)
            {
                go.gameObject?.SetActive(!isActive);
            }

            foreach (var go in _activeObstacleObject)
            {
                go.gameObject?.SetActive(isActive);
            }
        }
        else
        {
            foreach (var go in _activeObstacleObject)
            {
                go.gameObject?.SetActive(isActive);
            }

            foreach (var go in _inactiveObstacleObject)
            {
                go.gameObject?.SetActive(!isActive);
            }
        }
        //_obstacleObject.gameObject?.SetActive(isActive);       
    }
}
