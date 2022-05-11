using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _squadEditorCam;
    [SerializeField] private GameObject _mainMenuCam;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _logo;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private TMP_Text _loadingText;

    public void GoToSquadEditor()
    {
        if (_squadEditorCam.activeInHierarchy) return;

        _buttons.SetActive(false);
        _logo.SetActive(false);
        _squadEditorCam.SetActive(true);
        _mainMenuCam.SetActive(false);
    }
    public void GoToMainMenu()
    {
        if (_mainMenuCam.activeInHierarchy) return;
        _squadEditorCam.SetActive(false);
        _mainMenuCam.SetActive(true);
    }

    public void EnterQueue()
    {
        StartCoroutine(QueueUp());
    }

    private IEnumerator QueueUp()
    {
        _loadingScreen.SetActive(true);
        yield return SceneManager.LoadSceneAsync(1);
    }
}
