using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObjectSearcher : MonoBehaviour
{
   [SerializeField,Tooltip("�t�B�[���h�I�u�W�F�N�g�̃��C���[")]
    LayerMask mask = default;

    /// <summary>
    /// �ڂ̑O�ɃI�u�W�F�N�g�����邩�𔻒肷��֐�
    /// </summary>
    public void Search(float h,float v)
    {
        //�v�ύX�A�����Ō��̃I�u�W�F�N�g�����肷��
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);

        if (hit.collider)
        {
            hit.collider.GetComponent<IAction>().Action();
        }
    }
}
