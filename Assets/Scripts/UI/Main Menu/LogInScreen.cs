using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class LogInScreen : MonoBehaviour
{
    private const string LogInKey = "KeepLogIn";

    // Use player prefs to check if player should save prefs, if so check if data exists, if it does auto log in
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private Button _emailResetButton;
    [SerializeField] private string _emailResetUrl;

    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _passwordResetButton;
    [SerializeField] private string _passwordResetUrl;

    [SerializeField] private Toggle _keepLoginInfo;

    [SerializeField] private MenuDoors _menuDoors;
    [SerializeField] private FoldingBar _screenBar;
    [SerializeField] private GameObject _loadingWindow;
    [SerializeField] private TMP_Text _loadingText;


    private void Start()
    {
        if(PlayerPrefs.HasKey(LogInKey))
        {
            if(PlayerPrefs.GetInt(LogInKey) == 1)
            {
                //TODO: Deserialize the log in data Json here and set the returning data as the password and email field text 

                TryLogIn();
            }
        }
    }

    public void OpenPasswordReset()
    {
        OpenURL(_passwordResetUrl);
    }
    public void OpenEmailReset()
    {
        OpenURL(_emailResetUrl);
    }
    private void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void TryLogIn()
    {
        StartCoroutine(LogIn());
    }

    private IEnumerator LogIn()
    {
        _loadingWindow.SetActive(true);
        _loadingText.text = "Logging you in...";
        yield return new WaitForSeconds(0.5f);
        // All server logic goes here
        if(_keepLoginInfo.isOn)
        {
            //Serialize the login info here if autolog is on;
            _loadingText.text = "Saving Login Info...";
            PlayerPrefs.SetInt(LogInKey, 1);
        }
        // The waits are to simulate the lenght of the actual operation
        yield return new WaitForSeconds(0.25f);
        _loadingWindow.SetActive(false);
        _menuDoors.Open();
        _screenBar.Toggle(false);
    }
}
