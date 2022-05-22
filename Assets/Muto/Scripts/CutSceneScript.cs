using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneScript : MonoBehaviour
{
    [SerializeField] string _togetherTag = "Together";
    [SerializeField] string _playerTag = "Player";
    [SerializeField] PlayableDirector _timeLine = default;
    [SerializeField] Transform _timeLineHumanPos = default;
    [SerializeField] Transform _timeLineGhostPos = default;

    TimeLineManager _manager;
    [SerializeField]bool isPlay;
    [SerializeField] bool isEnding;

    private void Start()
    {
        _manager = TimeLineManager.Instance;

        if(isPlay)
        {
            _manager.StartCutScene(_timeLine);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    public void EndCutScene()
    {
        _manager.EndCutScene(_timeLineHumanPos, _timeLineGhostPos);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPlay)
        {
            if (!isEnding)
            {
                if (collision.CompareTag(_togetherTag) || collision.CompareTag(_playerTag))
                {
                    if (_timeLine)
                    {
                        isPlay = true;
                        _manager.StartCutScene(_timeLine);
                    }
                    else
                    {
                        Debug.LogError("タイムラインを入れてください");
                    }
                }
            }
            else
            {
                if (collision.CompareTag(_togetherTag))
                {
                    if (_timeLine)
                    {
                        isPlay = true;
                        _manager.StartCutScene(_timeLine);
                    }
                    else
                    {
                        Debug.LogError("タイムラインを入れてください");
                    }
                }
            }
        }
    }
    public void Goal()
    {
        SoundManager.Instance.CriAtomStop();
        SoundManager.Instance.CriAtomPlay(CueSheet.ME, "Clear");
        FieldManager.Instance.Clear();
    }
}
