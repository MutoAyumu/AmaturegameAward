using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] Image _fadePanal = default;
    [SerializeField] float _fadeTime = 2f;
    [SerializeField] GameObject _timeLinePanal = default;
    [SerializeField] Canvas _playerCanvas = default;
    [SerializeField] Transform _cutSceneGhost = default;

    bool IsCutScene;
    HumanController _human = default;
    GhostController _ghost = default;

    public bool CutSceneFlag { get => IsCutScene; }

    private void Start()
    {
        _human = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
        //_cutSceneHuman.gameObject.SetActive(false);
        //_cutSceneGhost.gameObject.SetActive(false);
    }

    /// <summary>
    /// カットシーンを始める
    /// </summary>
    /// <param name="cut"></param>
    public void StartCutScene(PlayableDirector cut)
    {
        ChangeFlag();
        FadePanel(cut);
    }
    public void EndCutScene(Transform pos1, Transform pos2)
    {
        FadePanal(pos1, pos2);
    }
    /// <summary>
    /// タイムラインの開始と終了で呼ぶ
    /// </summary>
    public void ChangeFlag()
    {
        IsCutScene = !IsCutScene;
    }
    void FadePanel(PlayableDirector cut)
    {
        if (_fadePanal.color.a == 0)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    FadePanel(cut);
                    _timeLinePanal.SetActive(true);
                    _playerCanvas.gameObject.SetActive(false);
                    _human.gameObject.SetActive(false);
                    _ghost.gameObject.SetActive(false);
                    //_cutSceneHuman.gameObject.SetActive(true);
                    //_cutSceneGhost.gameObject.SetActive(true);
                    cut.Play();
                });
        }
        else
        {
            _fadePanal.DOFade(0, _fadeTime)
                .OnComplete(() =>
                {
                    //cut.Play();
                });
        }
    }
    void FadePanal(Transform setPos1, Transform setPos2)
    {
        if (_fadePanal.color.a == 0)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    FadePanal(setPos1, setPos2);
                    _human.gameObject.SetActive(true);
                    _ghost.gameObject.SetActive(true);
                    _human.transform.position = setPos1.position;
                    _ghost.transform.position = setPos2.position;
                    //_cutSceneHuman.gameObject.SetActive(false);
                    //_cutSceneGhost.gameObject.SetActive(false);
                    _timeLinePanal.SetActive(false);
                    _playerCanvas.gameObject.SetActive(true);
                });
        }
        else
        {
            _fadePanal.DOFade(0, _fadeTime)
                .OnComplete(() =>
                {
                    ChangeFlag();
                });
        }
    }
}
