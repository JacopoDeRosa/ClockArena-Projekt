using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class LoggedUser
{
    private static bool _isLogged;
    private static UserData _userData;

    public static UserData UserData { get => _userData; }

    public static event Action<UserData> onUserLoggedIn;

    public static void LogInUser(UserData user)
    {
        _userData = user;
        _isLogged = true;
        onUserLoggedIn?.Invoke(user);
    }
}

[System.Serializable]
public class UserData
{
    public string userName;
    public string displayName;
    public int image;
    public int rCoins;
    public int aCoins;
  
}


