using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : CharacterControllerBase
{
    public override void OnUpdate()
    {
        //�����Ńv���C���[�ɒǏ]
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //�����Ńv���C���[�̉E���Ƃ��Ɉړ�������
        }
    }
}
