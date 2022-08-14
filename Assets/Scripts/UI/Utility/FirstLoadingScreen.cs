using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadingScreen : MonoBehaviour
{
    [SerializeField] private int _firstSceneIndex;


    void Start()
    {
        StartCoroutine(GameLoadRoutine());
    }


    private IEnumerator GameLoadRoutine()
    {
        yield return new WaitForSeconds(2);
        yield return SceneManager.LoadSceneAsync(_firstSceneIndex);     
    }
}
