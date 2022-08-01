using System.Collections;
using System.Collections.Generic;
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
}
