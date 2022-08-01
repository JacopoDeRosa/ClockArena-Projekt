using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TakeScreenShot : MonoBehaviour
{
    
    [Button]
    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screen_Capture.png");
        UnityEditor.AssetDatabase.Refresh();
    }

}
