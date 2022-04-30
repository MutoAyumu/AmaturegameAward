using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tama2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage();
        }
    }
}
