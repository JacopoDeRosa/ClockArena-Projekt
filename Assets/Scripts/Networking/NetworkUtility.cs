using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkUtility
{
    public const string signature = "5#@eK1GF9r*t";
    public const string dbUrl = "http://localhost/Clockwork_Database";

    public static WWWForm GetSignedForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("appPassword", signature);
        return form;
    }

    public static IEnumerator UpdateUserParameter(string parameter, string value)
    {
        if (LoggedUser.IsLogged == false) yield break;

        WWWForm form = GetSignedForm();

        form.AddField("user", LoggedUser.UserData.userName);
        form.AddField("parameter", parameter);
        form.AddField("value", value);

        UnityWebRequest webRequest = UnityWebRequest.Post(dbUrl + "/UpdateUserParameter.php", form);

        yield return webRequest.SendWebRequest();

        webRequest.Dispose();
    }

    public static IEnumerator UpdateUserParameter(string parameter, int value)
    {
        if (LoggedUser.IsLogged == false) yield break;

        WWWForm form = GetSignedForm();

        form.AddField("user", LoggedUser.UserData.userName);
        form.AddField("parameter", parameter);
        form.AddField("value", value);

        UnityWebRequest webRequest = UnityWebRequest.Post(dbUrl + "/UpdateUserParameter.php", form);

        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            Debug.LogError(webRequest.error);
        }

        webRequest.Dispose();
    }

    public static IEnumerator GetUserParameter(string parameter, Action<int> setCallback)
    {
        WWWForm form = GetSignedForm();
        form.AddField("user", LoggedUser.UserData.userName);
        form.AddField("parameter", parameter);

        UnityWebRequest webRequest = UnityWebRequest.Post(dbUrl + "/GetUserParameter.php", form);

        yield return webRequest.SendWebRequest();

        if(webRequest.error != null)
        {
            Debug.LogError(webRequest.error);
        }

        if(int.TryParse(webRequest.downloadHandler.text, out int result))
        {
            setCallback(result);
        }

        webRequest.Dispose();

    }

    public static IEnumerator GetUserParameter(string parameter, Action<string> setCallback)
    {
        WWWForm form = GetSignedForm();
        form.AddField("user", LoggedUser.UserData.userName);
        form.AddField("parameter", parameter);

        UnityWebRequest webRequest = UnityWebRequest.Post(dbUrl + "/GetUserParameter.php", form);

        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            Debug.LogError(webRequest.error);
        }

        string result = webRequest.downloadHandler.text;
       
        setCallback(result);

        webRequest.Dispose();
    }

    public static IEnumerator TrySpendACoins(int amount, Action<bool> callback)
    {
        int coinAmount = 0;

        void CheckCoinAmount(int coins)
        {
            coinAmount = coins;
        }

        yield return GetUserParameter("acoins", CheckCoinAmount);

        if (coinAmount < amount)
        {
            callback(false);
        }
        else
        {
            yield return UpdateUserParameter("acoins", coinAmount - amount);
            UserData data = LoggedUser.UserData;
            data.aCoins = coinAmount - amount;
            LoggedUser.UpdateUserData(data);
            callback(true);
        }
    }
}
