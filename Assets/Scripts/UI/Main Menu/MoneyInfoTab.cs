using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInfoTab : MonoBehaviour
{
    [SerializeField] private TMP_Text _aCoinsText, _rCoinsText;

    private void Awake()
    {
        LoggedUser.onUserUpdate += OnUserUpdate;
        LoggedUser.onUserLoggedIn += OnUserUpdate;
    }

    private void OnDestroy()
    {
        LoggedUser.onUserUpdate -= OnUserUpdate;
        LoggedUser.onUserLoggedIn -= OnUserUpdate;
    }

    private void OnUserUpdate(UserData user)
    {
        _aCoinsText.text = user.aCoins.ToString();
        _rCoinsText.text = user.rCoins.ToString();
    }
}
