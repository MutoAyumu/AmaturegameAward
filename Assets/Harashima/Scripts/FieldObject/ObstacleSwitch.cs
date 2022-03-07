using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSwitch : MonoBehaviour, IActivate
{
    [SerializeField,Tooltip("�X�C�b�`�ɑΉ�����")]
    ObstacleObjectController[] _obstacleObejects; 
    public void Action()
    {
        foreach(var i in _obstacleObejects)
        {
            i.Operation();
        }
        Debug.Log("�쓮");
    }
}
