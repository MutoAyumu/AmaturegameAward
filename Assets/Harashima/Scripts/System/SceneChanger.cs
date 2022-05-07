using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Image _fadePanel = default;
    [SerializeField] float _fadeTime = 1;
    bool IsFade;

    public void SceneChange(string name)
    {
        //StartCoroutine(LoadScene(name,2f));
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }

    private IEnumerator LoadScene(string _name, float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        AsyncOperation async = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Single);

        yield return async;

    }

    public void Fade(GameObject go)
    {
        if (!IsFade)
        {
            IsFade = true;
            _fadePanel.DOFade(1, _fadeTime)
                .OnComplete(() =>
                {
                    go.SetActive(true);
                    Fade(go);
                });
        }
        else
        {
            IsFade = false;
            _fadePanel.DOFade(0, _fadeTime);
        }
    }
    public void FadeSceneChange(string name)
    {
        _fadePanel.DOFade(1, _fadeTime)
            .OnComplete(() =>
            {
                SceneChange(name);
            });
    }
}
