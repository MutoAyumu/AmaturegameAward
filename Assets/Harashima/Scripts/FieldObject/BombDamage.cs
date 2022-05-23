using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHP>(out PlayerHP hp))
        {
            hp.Damage();
        }
    }
}
