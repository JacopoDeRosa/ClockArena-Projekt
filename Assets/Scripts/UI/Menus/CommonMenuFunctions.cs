using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CommonMenuFunctions : MonoBehaviour
{
    [Button]
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

    [Button]
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

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
