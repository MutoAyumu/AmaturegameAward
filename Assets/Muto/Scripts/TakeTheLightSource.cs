using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheLightSource : MonoBehaviour
{
    [SerializeField, Tooltip("�t�B�[���h�I�u�W�F�N�g�̃��C���[")]
    LayerMask mask = default;

    /// <summary>
    /// �H�삪�ڂ̑O�ɂ���������z�����邽�߂̊֐�
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void Take(float h, float v)
    {
        //�v�ύX�A�����Ō��̃I�u�W�F�N�g�����肷��
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);

        if (hit.collider)
        {
            hit.collider.GetComponent<MovingLightSource>().IsMoving();
        }
    }
}
