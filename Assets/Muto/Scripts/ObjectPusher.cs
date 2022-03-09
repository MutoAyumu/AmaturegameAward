using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    MoveBlock _block;
    bool _isGrab = false;

    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// </summary>
    public void Catch(float h, float v, float length, float grabbingSpeed, LayerMask layer)
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), length, layer);
        _block = hit.collider?.GetComponent<MoveBlock>();

        if (_block && !_isGrab)
        {
            _block.Rb.bodyType = RigidbodyType2D.Dynamic;
            _isGrab = true;
            CharacterManager.Instance.Human.CurrentSpeed = grabbingSpeed;
            Debug.Log("Catch");
        }
    }
    /// <summary>
    /// �{�^����������Ă��鎞�ɌĂ΂��
    /// </summary>
    public void MoveIt(float h, float v)
    {
        if (_block && _isGrab)
        {
            _block.Rb.velocity = new Vector2(h, v).normalized * CharacterManager.Instance.Human.CurrentSpeed;
            Debug.Log("Move");
        }
    }
    /// <summary>
    /// �{�^���������ꂽ���ɌĂ΂��
    /// </summary>
    public void Release(float moveSpeed)
    {
        if (_block && _isGrab)
        {
            _block.Rb.velocity = Vector2.zero;
            _block.Rb.bodyType = RigidbodyType2D.Kinematic;
            _block = null;
            _isGrab = false;
            CharacterManager.Instance.Human.CurrentSpeed = moveSpeed;
            Debug.Log("Release");
        }
    }
}
