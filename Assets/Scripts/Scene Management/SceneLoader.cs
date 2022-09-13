using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private LoadingScreen _loadingScreen;
    private Fader _fader;

    private void Awake()
    {
        _loadingScreen = FindObjectOfType<LoadingScreen>();
        _fader = FindObjectOfType<Fader>();
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneRoutine(index));
    }

    private IEnumerator LoadSceneRoutine(int index)
    {
        yield return _fader.FadeOutRoutine();

        yield return SceneManager.LoadSceneAsync(index);
    }
}
