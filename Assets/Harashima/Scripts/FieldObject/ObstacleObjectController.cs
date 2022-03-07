using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectController : MonoBehaviour
{
    [SerializeField, Tooltip("åªç›çÏìÆÇµÇƒÇ¢ÇÈÇ©")]
    bool isActive = false;

    [SerializeField, Tooltip("è·äQï®")]
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
