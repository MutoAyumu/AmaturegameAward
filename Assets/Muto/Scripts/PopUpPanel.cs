using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField] Image _panel = default;
    [SerializeField] Image _image = default;
    [SerializeField]Text _text = default;

    [SerializeField] Sprite _setSprite = default;
    [SerializeField, Multiline(5)] string _setText = default;

    [SerializeField] GameObject _activeObj = default;
    [SerializeField] string _inputButtonName = "Fire1";
    [SerializeField] string _playerTag = "Player";
    [SerializeField] string _togetherTag = "Together";
    bool isActive;

    private void Start()
    {
        _panel.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            if (collision.CompareTag(_playerTag) || collision.CompareTag(_togetherTag))
            {
                isActive = true;
                _image.sprite = _setSprite;
                _text.text = _setText;
                _panel.gameObject.SetActive(true);
                FieldManager.Instance.Test();
            }
        }
    }
    private void Update()
    {
        if(Input.GetButtonDown(_inputButtonName) && isActive)
        {
            _panel.gameObject.SetActive(false);

            if(_activeObj)
            _activeObj.SetActive(true);

            FieldManager.Instance.Test();
            Destroy(this.gameObject);
        }
    }
}
