using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Net.Mail;

public class LogInScreen : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool _deleteLogInAtStartInEditor;
#endif

    private const string LogInKey = "KeepLogIn";
    private const string EmailKey = "LogInMail";
    private const string PasswordKey = "LogInPass";

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
    [SerializeField] private LoadingScreen _loadingWindow;

    private void Start()
    {
#if UNITY_EDITOR
      if(_deleteLogInAtStartInEditor)  PlayerPrefs.DeleteKey(LogInKey);
#endif
        _passwordResetButton.onClick.AddListener(OpenPasswordReset);
        _emailResetButton.onClick.AddListener(OpenEmailReset);

        if(PlayerPrefs.HasKey(LogInKey) && PlayerPrefs.GetInt(LogInKey) == 1)
        {
            TryAutoLogin();
        }
    }

    private void TryAutoLogin()
    {
        string mail = PlayerPrefs.GetString(EmailKey);
        string password = PlayerPrefs.GetString(PasswordKey);

        if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(password))
        {
            return;
        }

        _emailField.text = mail;
        _passwordField.text = password; 

        TryLogIn();
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
        _loadingWindow.gameObject.SetActive(true);

        _loadingWindow.SetText("Logging you in...");

        yield return new WaitForSeconds(1f);

        // All server logic goes here

        if(_keepLoginInfo.isOn)
        {
            //Serialize the login info here if autolog is on;
            PlayerPrefs.SetString(EmailKey, _emailField.text);
            print(_emailField.text);
            print(_passwordField.text);
            PlayerPrefs.SetString(PasswordKey, _passwordField.text);
            _loadingWindow.SetText("Saving Login Info...");
            PlayerPrefs.SetInt(LogInKey, 1);
        }

        // The waits are to simulate the lenght of the actual operation

        yield return new WaitForSeconds(1f);

        _loadingWindow.SetText("Welcome in...");
        yield return new WaitForSeconds(1);

        _loadingWindow.gameObject.SetActive(false);

        yield return _screenBar.Toggle();

        _menuDoors.Open();
    }
}
