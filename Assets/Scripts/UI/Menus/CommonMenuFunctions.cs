using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExtendedUI;

public class CommonMenuFunctions : MonoBehaviour
{
    [SerializeField] private ConfirmationWindow _confirmationWindow;

    public void FoldAllBars()
    {
        FoldingBar[] bars = FindObjectsOfType<FoldingBar>();

        foreach (FoldingBar bar in bars)
        {
            if(bar.Open)
            {
                bar.Toggle(false);
            }
        }
    }
    public void UnFoldAllBars()
    {
        FoldingBar[] bars = FindObjectsOfType<FoldingBar>();

        foreach (FoldingBar bar in bars)
        {
            if (bar.Open == false)
            {
                bar.Toggle(true);
            }
        }
    }
    public void BackToMainMenu()
    {
        SceneLoader.Instance.LoadScene(0);
    }

    public void BackToMainMenuConf()
    {
        if (_confirmationWindow == null) return;
        var window = Instantiate(_confirmationWindow, transform);
        window.onConfirm += BackToMainMenu;
        window.SetMessage("Go Back to the menu?");
        FoldAllBars();
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEditor.EditorApplication.Beep();   
#endif
    }

    public void QuitConf()
    {
        if (_confirmationWindow == null) return;
        var window = Instantiate(_confirmationWindow, transform);
        window.onConfirm += Quit;
    }
  

}
