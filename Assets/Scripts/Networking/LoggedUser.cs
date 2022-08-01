using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoggedUser
{
    private static string _username;
    private static int _arenaCoins;
    private static int _premiumCoins;

    public static void SetUser(string username, int arenaCoins, int premiumCoins)
    {
        _username = username;
        _arenaCoins = arenaCoins;
        _premiumCoins = premiumCoins;
    }
}
