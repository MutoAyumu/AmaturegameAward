using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour, IActivate
{
    [SerializeField, Tooltip("現在作動しているか")]
    bool isActive = false;

    [SerializeField, Tooltip("作動した時に表示させるもの")]
    GameObject[] _activeObstacleObject;

    [SerializeField, Tooltip("作動していない時に表示させるもの")]
    GameObject[] _inactiveObstacleObject;

    private void Start()
    {
        if(isActive)    //作動
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
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, "DoorOpen");

        if (isActive)    //作動
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
