using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tama2 : MonoBehaviour
{
    [Header("‰¹")]
    [SerializeField] string _cueName = "RasuBossTama";
    [SerializeField] int _damage = 2;

    private void Start()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage(_damage);
        }
    }
}
