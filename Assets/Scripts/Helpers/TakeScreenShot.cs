using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour
{
    [SerializeField] private string _folderPath;
    [SerializeField] private string _fileName;

    [SerializeField]
    private string _finalFolderPath;
    
    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot(_finalFolderPath);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    private void OnValidate()
    {
        _finalFolderPath = Application.dataPath + "/" + _folderPath + "/" + _fileName + ".png";
    }

}
