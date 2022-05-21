using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IActivate
{
    [SerializeField, Tooltip("ƒŠƒXƒ|[ƒ“‚·‚éêŠ")]
    Transform[] _respawnArray;
    public void Action()
    {
        if(_respawnArray.Length>=2)
        {
            CharacterManager.Instance.SetResetPos(_respawnArray[0].position, _respawnArray[1].position);
        }     
    }
}
