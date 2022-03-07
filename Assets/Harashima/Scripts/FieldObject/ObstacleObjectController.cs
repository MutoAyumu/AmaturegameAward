using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour
{
    [SerializeField, Tooltip("現在作動しているか")]
    bool isActive = false;

    [SerializeField, Tooltip("障害物")]
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
