using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UserLogIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameField, _passwordField;
    [SerializeField] private Button _logInButton;
    [SerializeField] private TMP_Text _errorText;

    private void Awake()
    {
        _logInButton.onClick.AddListener(LogInUser);
    }

    private void LogInUser()
    {
        StartCoroutine(LogInRoutine());
    }

    private IEnumerator LogInRoutine()
    {
        WWWForm logInForm = NetworkUtility.GetSignedForm();
        logInForm.AddField("userName", _userNameField.text);
        logInForm.AddField("password", _passwordField.text);

        UnityWebRequest logInRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/LogIn.php", logInForm);
        yield return logInRequest.SendWebRequest();

        switch(logInRequest.downloadHandler.text)
        {             
            case "3":
                LogError("Username could not be found");
                logInRequest.Dispose();
                yield break;
            case "4":
                LogError("Password is incorrect");
                logInRequest.Dispose();
                yield break;
        }

        UserData data = JsonUtility.FromJson<UserData>(logInRequest.downloadHandler.text);
        LoggedUser.LogInUser(data);
        logInRequest.Dispose();
    }

    private void LogError(string error)
    {
        _errorText.text = error;
    }
}
