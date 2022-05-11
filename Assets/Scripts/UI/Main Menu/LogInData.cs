using System;
using UnityEngine;


public class LogInData
{
    private string _mail;
    private string _password;

    public string Email { get => _mail; }
    public string Password { get => _password; }

    public LogInData(string mail, string password)
    {
        _mail = mail;
        _password = password;
    }
}
