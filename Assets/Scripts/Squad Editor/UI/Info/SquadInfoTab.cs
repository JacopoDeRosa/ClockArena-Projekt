using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquadInfoTab : MonoBehaviour
{
    [SerializeField] private TMP_Text _squadNameText, _squadValueText;
    [SerializeField] private Image _squadLogo;
    [SerializeField] private GameObject _emptySquadTab, _fullSquadTab;


    private SquadEditor _editor;


    private void Awake()
    {
        _editor = FindObjectOfType<SquadEditor>();
        _editor.onSquadLoaded += OnSquadLoaded;
    }

    private void OnSquadLoaded(SquadData squadData)
    {
        if(squadData == null)
        {
            _fullSquadTab.SetActive(false);
            _emptySquadTab.SetActive(true);
        }
        else
        {
            _fullSquadTab.SetActive(true);
            _emptySquadTab.SetActive(false);
            _squadNameText.text = squadData.name;
            _squadLogo.sprite = GameItemDB.GetDbOfType<IconsDB>(IconsDB.Name).GetItem(squadData.image);
        }
    }
}
