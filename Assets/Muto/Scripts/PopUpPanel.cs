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
    bool isActive;

    private void Start()
    {
        _panel.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            isActive = true;
            _image.sprite = _setSprite;
            _text.text = _setText;
            _panel.gameObject.SetActive(true);
            FieldManager.Instance.Test();
        }
    }
    private void Update()
    {
        if(Input.GetButtonDown(_inputButtonName) && isActive)
        {
            _panel.gameObject.SetActive(false);
            _activeObj.SetActive(true);
            FieldManager.Instance.Test();
            Destroy(this.gameObject);
        }
    }
}
