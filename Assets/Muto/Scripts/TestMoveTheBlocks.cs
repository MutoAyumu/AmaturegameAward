using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveTheBlocks : MonoBehaviour
{
    [SerializeField] LayerMask mask = default;
    [SerializeField] float _rayLength = 1f;
    TestMovingBlock _test = default;

    /*ToDo
        input���R���g���[���ɔC����
        �L���X�g����
        Ray����Ȃ���Trigger�ɂ��邩��
    */

    //public void Move(float h, float v)
    //{
    //    if (_test)
    //    {
    //        Debug.Log("�u���b�N�𓮂���");  //�u���b�N�𓮂������͓����X�s�[�h��ς���
    //        _test.Rb.velocity = new Vector2(h, v).normalized * 3f;
    //    }
    //}
    //public void Seize(float h, float v)
    //{
    //    Vector2 origin = this.transform.position;
    //    RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(h, v), _rayLength, mask);
    //    var block = hit.collider?.GetComponent<TestMovingBlock>();

    //    if (block)
    //    {
    //        _test = block;
    //        _test.Rb.bodyType = RigidbodyType2D.Dynamic;
    //        CharacterManager._instance.Human.IsSeize = true;
    //        Debug.Log("�͂�");
    //    }
    //}
    //public void Separate()
    //{
    //    if (_test)
    //    {
    //        _test.Rb.velocity = Vector2.zero;
    //        _test.Rb.bodyType = RigidbodyType2D.Kinematic;
    //        _test = null;
    //        CharacterManager._instance.Human.IsSeize = false;
    //        Debug.Log("������");
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer != mask && CharacterManager._instance.Human.IsSeize)
    //    {
    //        Debug.Log("Trigger����o��");
    //        Separate();
    //    }
    //}
}
