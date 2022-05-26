using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    [SerializeField]
    GameObject _buttonPrefab = null;
    [SerializeField]
    int _stageValue = 10;

    [SerializeField]
    SceneChanger _sceneChanger = null;
    // Start is called before the first frame update
    void Start()
    {
        var gm = GameManager.Instance;
        _stageValue = gm.StageLimit;
        if(_buttonPrefab)
        {
            for (int i = 1;i<=_stageValue;i++)
            {
                var button = Instantiate(_buttonPrefab, this.transform);
                var buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = $"ステージ{i}";
                
                if (gm.ClearedStage[i-1])
                {                   
                    buttonText.GetComponentInChildren<Image>().enabled = false;
                    var b = button.GetComponent<Button>();
                    b.enabled = true;
                    int num = i;
                    b.onClick.AddListener(() => _sceneChanger.SceneChange($"Scene{num}"));
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
