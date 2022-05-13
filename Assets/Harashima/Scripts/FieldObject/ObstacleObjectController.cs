using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour, IActivate
{
    [SerializeField, Tooltip("���ݍ쓮���Ă��邩")]
    bool isActive = false;

    [SerializeField, Tooltip("�쓮�������ɕ\�����������")]
    GameObject[] _activeObstacleObject;

    [SerializeField, Tooltip("�쓮���Ă��Ȃ����ɕ\�����������")]
    GameObject[] _inactiveObstacleObject;

    [SerializeField] CriAtomSource _criAtom = default;

    private void Start()
    {
        if(isActive)    //�쓮
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

        if (isActive)    //�쓮
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
