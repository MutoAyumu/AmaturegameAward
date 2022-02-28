using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void SceneChange(string name)
    {
        StartCoroutine(LoadScene(name,2f));
    }

    private IEnumerator LoadScene(string _name, float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

        AsyncOperation async = SceneManager.LoadSceneAsync(_name, LoadSceneMode.Single);

        yield return async;

    }
}
