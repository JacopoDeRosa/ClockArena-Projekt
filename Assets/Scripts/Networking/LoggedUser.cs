using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class LoggedUser
{
    private static bool _isLogged;
    private static UserData _userData;
    private static SquadData _userSquad;

    public static UserData UserData { get => _userData; }
    public static event Action<UserData> onUserLoggedIn;
    public static void LogInUser(UserData user)
    {
        _userData = user;
        _isLogged = true;
        onUserLoggedIn?.Invoke(user);
    }
    public static void ChangeArenaCoins(int coins)
    {
        _userData.aCoins += coins;
    }
    public static void ChangeRoyalCoins(int coins)
    {
        _userData.rCoins += coins;
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


