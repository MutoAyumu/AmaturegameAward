using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UnityEngine.EventSystems
{
    public class SelectScene : MonoBehaviour
    {
        [SerializeField]
        GameObject _buttonPrefab = null;

        int _stageValue = 9;

        [SerializeField]
        SceneChanger _sceneChanger = null;

        [SerializeField]
        UnityEvent _onBackButtonClickEvent = new UnityEvent();

        Button _firstButon = null;

        void Start()
        {
            var gm = GameManager.Instance;
            _stageValue = gm.StageLimit-1;

            if (_buttonPrefab)
            {
                for (int i = 1; i <= _stageValue; i++)
                {
                    var button = Instantiate(_buttonPrefab, this.transform);
                    var buttonText = button.GetComponentInChildren<Text>();
                    if(i%3==0)
                    {
                        buttonText.text = $"��";
                    }
                    else
                    {
                        buttonText.text = $"�X�e�[�W{i}";
                    }
                    

                    if (gm.ClearedStage[i - 1])
                    {
                        buttonText.color = Color.white;
                         var b = button.GetComponent<Button>();
                        b.enabled = true;
                        int num = i;
                        b.onClick.AddListener(() => _sceneChanger.SceneChange($"Scene{num}"));
                        b.onClick.AddListener(()=> SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SystemDone"));
                        b.Select();
                        if(!_firstButon)
                        {
                            _firstButon = b;
                        }
                    }
                }
            }
            _firstButon.Select();
        }

        [SerializeField] EventSystem _eventSystem = null;
        [SerializeField]
        string _cueName = "SystemSelect";
        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cueName);
                _onBackButtonClickEvent.Invoke();
            }

            if (_eventSystem)
            {
                if (!_eventSystem.currentSelectedGameObject && Input.GetButtonDown("Fire1"))
                {
                    _firstButon.Select();
                }
                
            }

        }

        private void OnEnable()
        {
            if(_firstButon)
            {
                _firstButon.Select();
                _firstButon.GetComponent<TitleStageSelect>().ActiveSelectedUI();
            }
        }
    }
}
