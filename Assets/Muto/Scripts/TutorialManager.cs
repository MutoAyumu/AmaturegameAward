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
    [SerializeField] Transform _cutSceneHuman = default;
    [SerializeField] Transform _cutSceneGhost = default;

    bool IsCutScene;
    HumanController _human = default;

    public bool CutSceneFlag { get => IsCutScene; }

    private void Start()
    {
        _human = CharacterManager.Instance.Human;
        _cutSceneHuman.gameObject.SetActive(false);
        _cutSceneGhost.gameObject.SetActive(false);
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
    public void EndCutScene()
    {
        FadePanal();
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
                    _cutSceneHuman.gameObject.SetActive(true);
                    _cutSceneGhost.gameObject.SetActive(true);
                });
        }
        else
        {
            _fadePanal.DOFade(0, _fadeTime)
                .OnComplete(() =>
                {
                    cut.Play();
                });
        }
    }
    void FadePanal()
    {
        if (_fadePanal.color.a == 0)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    FadePanal();
                    _human.gameObject.SetActive(true);
                    _human.transform.position = _cutSceneHuman.position;
                    _cutSceneHuman.gameObject.SetActive(false);
                    _cutSceneGhost.gameObject.SetActive(false);
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
