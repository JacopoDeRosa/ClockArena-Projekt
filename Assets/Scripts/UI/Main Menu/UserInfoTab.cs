using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInfoTab : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayNameText, _usernameText;
    [SerializeField] private Image _image;

    private void Awake()
    {
        LoggedUser.onUserLoggedIn += OnUserLogIn;
    }

    private void OnUserLogIn(UserData userData)
    {
        _displayNameText.text = userData.displayName;
        _usernameText.text = "#" + userData.userName;
        _image.sprite = IconsDB.GetDbOfType<IconsDB>(IconsDB.Name).GetItem(userData.image);
    }
    
}
