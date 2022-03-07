using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour
{
    [SerializeField, Tooltip("���ݍ쓮���Ă��邩")]
    bool isActive = false;

    [SerializeField, Tooltip("��Q��")]
    GameObject _obstacleObject;

    private void Start()
    {
        _obstacleObject.gameObject?.SetActive(isActive);
    }

    public void Operation()
    {
        isActive = !isActive;
        _obstacleObject.gameObject?.SetActive(isActive);       
    }

}
