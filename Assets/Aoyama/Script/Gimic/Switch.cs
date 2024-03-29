using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Switch : MonoBehaviour, IActivate
{
    [SerializeField, Tooltip("作動させたいギミック")]
    GameObject _gimic;

    public void Action()
    {
        _gimic.GetComponent<IGimic>().Activate();
    }
}
