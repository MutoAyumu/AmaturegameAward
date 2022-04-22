using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField] Image _panelImage = default;
    bool isActive;

    private void Start()
    {
        _panelImage.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            isActive = true;
            _panelImage.gameObject.SetActive(true);
        }
    }
}
