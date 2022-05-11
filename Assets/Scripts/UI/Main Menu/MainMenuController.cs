using System.Collections;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _squadEditorCam;
    [SerializeField] private GameObject _mainMenuCam;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _logo;

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
}
