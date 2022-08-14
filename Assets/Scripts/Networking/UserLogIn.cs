using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserLogIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameField, _passwordField;
    [SerializeField] private Button _logInButton;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private Fader _fader;
    [SerializeField] private GameObject _screen;

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
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetText("Connecting To Server");
        WWWForm logInForm = NetworkUtility.GetSignedForm();
        logInForm.AddField("userName", _userNameField.text);
        logInForm.AddField("password", _passwordField.text);

        UnityWebRequest logInRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/LogIn.php", logInForm);
        yield return logInRequest.SendWebRequest();

        if (logInRequest.error == null)
        {
            switch (logInRequest.downloadHandler.text)
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
        }
        else
        {
            LogError("Server Connection Error");         
            yield break;
        }

        _loadingScreen.SetText("Logging You In");

        UserData data = JsonUtility.FromJson<UserData>(logInRequest.downloadHandler.text);
        LoggedUser.LogInUser(data);
        logInRequest.Dispose();
        yield return _fader.FadeOutRoutine();
        _loadingScreen.gameObject.SetActive(false);
        _screen.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        yield return _fader.FadeInRoutine();
    }

    private void LogError(string error, bool disableLoading = true)
    {
        _errorText.text = error;
        if(disableLoading) _loadingScreen.gameObject.SetActive(false);
    }
}
