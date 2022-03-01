using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheLightSource : MonoBehaviour
{
    [SerializeField, Tooltip("�t�B�[���h�I�u�W�F�N�g�̃��C���[")]
    LayerMask mask = default;
    [SerializeField, Tooltip("�ێ��ł�������̏��")] int _upperLimit = 5;

    /// <summary>
    /// �H�삪�ڂ̑O�ɂ���������z��������߂����肷�邽�߂̊֐�
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void delivery(float h, float v)
    {
        //�v�ύX�A�����Ō��̃I�u�W�F�N�g�����肷��
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), 5f, mask);
        var light = hit.collider.GetComponent<MovingLightSource>();

        if (hit.collider && light.IsOn && CharacterManager._instance.Ghost._lightNum < _upperLimit)
        {
            CharacterManager._instance.Ghost.Stop();
            light.IsMoving();
        }
        else if(hit.collider && !light.IsOn && CharacterManager._instance.Ghost._lightNum > 0)
        {
            CharacterManager._instance.Ghost.Stop();
            light.IsMoving();
        }
    }
}
