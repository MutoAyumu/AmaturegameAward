using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    MoveBlock _block;
    bool _isGrab = false;

    /// <summary>
    /// ボタンが押された時に呼ばれる
    /// </summary>
    public bool Catch(float h, float v, float length, float grabbingSpeed, LayerMask layer, Animator anim)
    {
        Vector2 origin = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), length, layer);
        _block = hit.collider?.GetComponent<MoveBlock>();

        if (_block && !_isGrab)
        {
            _block.Rb.bodyType = RigidbodyType2D.Dynamic;
            _isGrab = true;
            CharacterManager.Instance.Human.CurrentSpeed = grabbingSpeed;
            anim.SetBool("IsAction", true);
            Debug.Log("Catch");
            return true;
        }

        return false;
    }
    /// <summary>
    /// ボタンが押されている時に呼ばれる
    /// </summary>
    public void MoveIt(float h, float v, float length, Animator anim, float speed, LayerMask layer, float lh, float lv)
    {
        if (_block && _isGrab)
        {
            _block.Rb.velocity = new Vector2(h, v).normalized * CharacterManager.Instance.Human.CurrentSpeed;
            Debug.Log("Move");

            Vector2 origin = this.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(lh, lv), length, layer);

            if (!hit.collider?.GetComponent<MoveBlock>())
            {
                Release(speed, anim);
            }
        }
    }
    /// <summary>
    /// ボタンが離された時に呼ばれる
    /// </summary>
    public bool Release(float moveSpeed, Animator anim)
    {
        if (_block && _isGrab)
        {
            _block.Rb.velocity = Vector2.zero;
            _block.Rb.bodyType = RigidbodyType2D.Kinematic;
            _block = null;
            _isGrab = false;
            CharacterManager.Instance.Human.CurrentSpeed = moveSpeed;
            anim.SetBool("IsAction", false);
            Debug.Log("Release");
            //return true;
        }

        return true;
    }
}
