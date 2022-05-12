using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.UI;

public class TimeLineManager : Singleton<TimeLineManager>
{
    [SerializeField] Image _fadePanal = default;
    [SerializeField] float _fadeTime = 2f;
    [SerializeField] GameObject _timeLinePanal = default;
    [SerializeField] Canvas _playerCanvas = default;

    bool IsCutScene;
    bool IsFade;
    [SerializeField] bool IsStart = true;
    HumanController _human = default;
    GhostController _ghost = default;

    public bool CutSceneFlag { get => IsCutScene; }

    private void Start()
    {
        if (IsStart)
        {
            _fadePanal.color = new Color(0, 0, 0, 1);
            _fadePanal.DOFade(0, _fadeTime).OnComplete(() => IsStart = false);
        }

        _human = CharacterManager.Instance.Human;
        _ghost = CharacterManager.Instance.Ghost;
    }

    /// <summary>
    /// カットシーンを始める
    /// </summary>
    /// <param name="cut"></param>
    public void StartCutScene(PlayableDirector cut)
    {
        StartCoroutine(OnCutScene(cut));
        Debug.Log("OnCutSceneが開始します");
    }
    IEnumerator OnCutScene(PlayableDirector cut)
    {
        while(true)
        {
            yield return 0;

            if(!IsStart)
            break;
        }

        Debug.Log("OnCutSceneが終了しました");
        ChangeFlag();
        FadePanel(cut);
        yield return 0;
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
        Debug.Log("フェードが開始します");

        if (!IsFade)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    IsFade = true;
                    FadePanel(cut);
                    _timeLinePanal.SetActive(true);
                    _playerCanvas.gameObject.SetActive(false);
                    _human.gameObject.SetActive(false);
                    _ghost.gameObject.SetActive(false);
                    cut.Play();
                });
        }
        else
        {
            _fadePanal.DOFade(0, _fadeTime)
                .OnComplete(() =>
                {
                    IsFade = false;
                });
        }
    }
    void FadePanal(Transform setPos1, Transform setPos2)
    {
        if (!IsFade)
        {
            _fadePanal.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    IsFade = true;
                    FadePanal(setPos1, setPos2);
                    _human.gameObject.SetActive(true);
                    _ghost.gameObject.SetActive(true);
                    _human.transform.position = setPos1.position;
                    _ghost.transform.position = setPos2.position;
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
                    IsFade = false;
                });
        }
    }
}
