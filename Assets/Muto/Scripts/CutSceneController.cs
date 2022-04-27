using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : Singleton<CutSceneController>
{
    [SerializeField] string _playerTag = "Player";
    [SerializeField] PlayableDirector _timeLine = default;

    TutorialManager _manager;
    [SerializeField]bool isPlay;

    private void Start()
    {
        _manager = TutorialManager.Instance;

        if(isPlay)
        {
            _manager.StartCutScene(_timeLine);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_playerTag) && !isPlay)
        {
            if (_timeLine)
            {
                isPlay = true;
                _manager.StartCutScene(_timeLine);
            }
            else
            {
                Debug.LogError("�^�C�����C�������Ă�������");
            }
        }
    }
    public void EndCutScene()
    {
        _manager.EndCutScene();
    }
}
