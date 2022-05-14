using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage();
        }
    }
}
