using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : CharacterControllerBase
{
    public override void OnUpdate()
    {
        //ここでプレイヤーに追従
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //ここでプレイヤーの右後ろとかに移動させる
        }
    }
}
