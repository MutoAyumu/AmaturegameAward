using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �A�C�e����UI�̍X�V������N���X(��)
/// </summary>
public class ItemUIManager : Singleton<ItemUIManager>
{
    [SerializeField, Tooltip("�A�C�e����\������p�l��")]
    GameObject[] _inventryPanels;
    public GameObject[] InventryPanels => _inventryPanels;

    [SerializeField, Tooltip("�A�C�e����\������C���[�W")]
    GameObject[] _inventryImage;

    [SerializeField, Tooltip("�A�C�e���̌���\������UIText")]
    Text[] _texts;
    public Text[] Texts => _texts;

    public void ChangeTextValue(int index, int value)
    {
        if(_texts[index])
        {
            _texts[index].text = value.ToString();
        }        
    }

    public void FirstGet(int index)
    {
        if(_inventryImage[index]&&_texts[index])
        {
            _inventryImage[index].SetActive(true);
            _texts[index].gameObject.SetActive(true);
        }
    }

    float _timer = 1f;
    [SerializeField]
    float _timeInterval = 1f;
    /// <summary>
    /// �f�o�b�O�p�B�C���v�b�g���󂯎��֐��i���j
    /// </summary>
    //void ItemInput()
    //{
    //    _timer += Time.deltaTime;
    //    float h = Input.GetAxis("Debug Horizontal");
    //    float v = Input.GetAxis("Debug Vertical");
    //    if (_timer > _timeInterval)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Z) || h < 0)
    //        {
    //            ItemManager.Instance.UseItem(0);
    //            _timer = 0f;
    //        }
    //        else if (Input.GetKeyDown(KeyCode.X) || h > 0)
    //        {
    //            ItemManager.Instance.UseItem(1);
    //            _timer = 0f;
    //        }
    //        //else if (Input.GetKeyDown(KeyCode.C) || v < 0)
    //        //{
    //        //    ItemManager.Instance.UseItem(2);
    //        //    _timer = 0f;
    //        //}
    //        //else if (Input.GetKeyDown(KeyCode.V) || h < 0)
    //        //{
    //        //    ItemManager.Instance.UseItem(3);
    //        //    _timer = 0f;
    //        //}
    //    }
    //}
}
