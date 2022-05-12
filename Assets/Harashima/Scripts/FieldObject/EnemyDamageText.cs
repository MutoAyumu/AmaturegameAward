using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageText : MonoBehaviour
{
    [SerializeField] Animator _textAnim = default;
    [SerializeField] Text _damageText = default;

    private void Start()
    {
        if (!_textAnim){
            _textAnim = GetComponent<Animator>();
        }
        if(!_damageText)
        {
            _damageText = GetComponent<Text>();
        }
    }
    public void DamageText(int damage)
    {    
        if (_damageText)
        {
            _damageText.gameObject.SetActive(true);
            _damageText.text = damage.ToString();
            _textAnim.Play("DamageText");
        }
    }
}
