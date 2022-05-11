using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CommonMenuFunctions : MonoBehaviour
{
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

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEditor.EditorApplication.Beep();   
#endif
    }
  

}
