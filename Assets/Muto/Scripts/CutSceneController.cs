using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : Singleton<CutSceneController>
{
    [SerializeField] string _playerTag = "Player";
    [SerializeField] PlayableDirector _timeLine = default;
    [SerializeField] Transform _setPos = default;

    TutorialManager _manager;
    bool isPlay;

    private void Start()
    {
        _manager = TutorialManager.Instance;
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
                Debug.LogError("ƒ^ƒCƒ€ƒ‰ƒCƒ“‚ð“ü‚ê‚Ä‚­‚¾‚³‚¢");
            }
        }
    }
    public void EndCutScene()
    {
        _manager.EndCutScene(_setPos);
    }
}
