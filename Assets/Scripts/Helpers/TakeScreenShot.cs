using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TakeScreenShot : MonoBehaviour
{
    [SerializeField] private string _folderPath;
    [SerializeField] private string _fileName;
    
    [Button]
    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/" + _folderPath + "/" + _fileName + ".png");
        UnityEditor.AssetDatabase.Refresh();
    }

}
