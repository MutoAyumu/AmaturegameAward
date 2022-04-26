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

    bool IsCutScene;

    public bool CutSceneFlag { get => IsCutScene; }

    /// <summary>
    /// カットシーンを始める
    /// </summary>
    /// <param name="cut"></param>
    public void StartCutScene(PlayableDirector cut)
    {
        ChangeFlag();
        FadePanel(cut);
    }
    public void EndCutScene(Transform t)
    {
        FadePanal(t);
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
    void FadePanal(Transform t)
    {
        if (_fadePanal.color.a == 0)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    FadePanal(t);
                    CharacterManager.Instance.Human.transform.position = t.position;
                    _timeLinePanal.SetActive(false);
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
