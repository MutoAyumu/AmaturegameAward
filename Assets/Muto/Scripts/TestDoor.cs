using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour, IActivate
{
    public void Action()
    {
        this.gameObject.SetActive(true);
        Debug.Log("ŠJ‚¢‚½");
    }
}
