using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObjectSearcher : MonoBehaviour
{
   [SerializeField,Tooltip("�t�B�[���h�I�u�W�F�N�g�̃��C���[")]
    LayerMask mask = default;
    private void Update()
    {
        Vector2 origin = this.transform.position;
        Debug.DrawLine(origin, origin + new Vector2(0, 1));
        if (Input.GetKeyDown(KeyCode.Space))
        {           
            Search();
        }
        
    }
    /// <summary>
    /// �ڂ̑O�ɃI�u�W�F�N�g�����邩�𔻒肷��֐�
    /// </summary>
    void Search()
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(0, 1), 5f, mask);
        if (hit.collider)
        {
            hit.collider.GetComponent<IAction>().Action();
        }
    }
}
