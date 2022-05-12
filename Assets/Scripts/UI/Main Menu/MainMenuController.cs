using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _squadEditorCam;
    [SerializeField] private GameObject _mainMenuCam;
    [SerializeField] private FoldingBar[] _mainMenuObjects;
    [SerializeField] private FoldingBar[] _selectorObjects;
    [SerializeField] private MenuPanelsController _mainMenuPanels;
    [SerializeField] private LoadingScreen _loadingScreen;

    public void GoToSquadEditor()
    {
        if (_squadEditorCam.activeInHierarchy) return;
        foreach (FoldingBar menu in _mainMenuObjects)
        {
            menu.Toggle(false);
        }
        foreach (FoldingBar menu in _selectorObjects)
        {
            menu.Toggle(true);
        }
        StartCoroutine(_mainMenuPanels.CloseActiveMenu());
        _squadEditorCam.SetActive(true);
        _mainMenuCam.SetActive(false);
    }
    public void GoToMainMenu()
    {
        if (_mainMenuCam.activeInHierarchy) return;
        foreach (FoldingBar menu in _mainMenuObjects)
        {
            menu.Toggle(true);
        }
        foreach (FoldingBar menu in _selectorObjects)
        {
            menu.Toggle(false);
        }
        _squadEditorCam.SetActive(false);
        _mainMenuCam.SetActive(true);
    }

    public void EnterQueue()
    {
        StartCoroutine(QueueUp());
    }



    private IEnumerator QueueUp()
    {
        yield return _mainMenuPanels.CloseActiveMenu();
        _loadingScreen.gameObject.SetActive(true);      
        _loadingScreen.SetText("In Queue...");
        yield return SceneManager.LoadSceneAsync(1);
    }
}
