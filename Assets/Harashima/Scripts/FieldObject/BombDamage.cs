using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombDamage : MonoBehaviour
{
    [SerializeField]
    string _cueName = "";
    [SerializeField]
    float _delayTime = 1.5f;
    private void Start()
    {
        DOVirtual.DelayedCall(_delayTime,() => 
        {
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);
        });
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHP>(out PlayerHP hp))
        {
            hp.Damage();
        }
    }
}
