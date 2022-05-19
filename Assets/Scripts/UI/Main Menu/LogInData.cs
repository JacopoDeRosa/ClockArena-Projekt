using System;
using UnityEngine;


[Serializable]
public class LogInData
{
    public string mail;
    public string password;

    public LogInData(string mail, string password)
    {
        this.mail = mail;
        this.password = password;
    }
}
