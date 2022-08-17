using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SquadEditor : MonoBehaviour
{
    [SerializeField] private Character _characterTemplate;
    [SerializeField] private Transform[] _characterBases = new Transform[6];
    [SerializeField] private Character[] _characters = new Character[6];
    [SerializeField] private GameObject _inspectionCamera;
    [SerializeField] private CharacterAbilitiesWindow _abilitiesWindow;
    [SerializeField] private CharacterEquipmentWindow _equipmentWindow;


    private SquadData _squad;
    private bool _focused;
    private LoadingScreen _loadingScreen;

    public bool SquadLoaded { get => _squad != null; }

    public event Action<SquadData> onSquadLoaded;

    private void Awake()
    {
        LoggedUser.onUserLoggedIn += TryGetSquad;
        _loadingScreen = FindObjectOfType<LoadingScreen>(true);
    }

    public SquadData CreateNewSquad(Faction faction, string name)
    {
        SquadData squadData = new SquadData(faction, name);
        _squad = squadData;
        StartCoroutine(UpdatePlayerSquad());
        return _squad;
    }

    public Character GetCharacter(int index)
    {
        if(index >= _characters.Length)
        {
            return null;
        }
        else
        {
            return _characters[index];
        }
    }
    public Character SpawnCharacterAtIndex(int index)
    {
        // TODO: Try to put everything in the corutine
        if (index >= _characterBases.Length) return null;

        Character spawnedChar = Instantiate(_characterTemplate, _characterBases[index]);

        _characters[index] = spawnedChar;

        spawnedChar.GetComponent<CharacterVoice>().PlayAcknowledge();

        StartCoroutine(AddCharacterRoutine(index));

        return spawnedChar;
    }
    public void FocusOnCharacter(int index)
    {
        if (_focused) return;
        _inspectionCamera.transform.rotation = Quaternion.identity;
        _inspectionCamera.transform.position = _characterBases[index].position;
        _inspectionCamera.gameObject.SetActive(true);
        _focused = true;
        _abilitiesWindow.FoldingBar.Toggle(true);
        _equipmentWindow.FoldingBar.Toggle(true);
    }
    public void LooseFocus()
    {
        if (_focused == false) return;
        _focused = false;
        _abilitiesWindow.FoldingBar.Toggle(false);
        _equipmentWindow.FoldingBar.Toggle(false);
        _inspectionCamera.SetActive(false);
    }

    private void TryGetSquad(UserData userData)
    {
        StartCoroutine(GetSquadRoutine(userData.userName));
    }

    private IEnumerator GetSquadRoutine(string owner)
    {
        WWWForm form = NetworkUtility.GetSignedForm();

        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetText("Loading User Squad");

        form.AddField("owner", owner);

#if UNITY_EDITOR
        yield return new WaitForSeconds(2);
#endif

        UnityWebRequest webRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/LoadUserSquad.php", form);

        yield return webRequest.SendWebRequest();

        SquadData squad = null;

        if (webRequest.downloadHandler.text == "3")
        {
            Debug.LogError("User has no squad assigned");
        }
        else if(webRequest.downloadHandler.text == "4")
        {
            Debug.LogError("Squad Query Error");
        }
        else
        {
            squad = JsonUtility.FromJson<SquadData>(webRequest.downloadHandler.text);
        }
      
        if(webRequest.error != null)
        {
            Debug.LogError("Server error in squad editor: " + webRequest.error);
        }

        if (squad != null)
        {
            for (int i = 0; i < squad.characters.Length; i++)
            {
                if (squad.characters[i].available == false)
                {
                    squad.characters[i] = null;
                }
            }

            // TODO: Place characters here.

            _squad = squad;
        }

        onSquadLoaded?.Invoke(squad);

        webRequest.Dispose();

        _loadingScreen.gameObject.SetActive(false);
    }
    private IEnumerator UpdatePlayerSquad()
    {
        if (LoggedUser.IsLogged == false)
        {
            Debug.Log("No User Logged");
            yield break;
        }

        _loadingScreen.gameObject.SetActive(true);

        _loadingScreen.SetText("Updating Squad Data");

#if UNITY_EDITOR
        yield return new WaitForSeconds(2);
#endif

        string squadJson = JsonUtility.ToJson(_squad);

        WWWForm form = NetworkUtility.GetSignedForm();

        form.AddField("owner", LoggedUser.UserData.userName);
        form.AddField("squad", squadJson);

        UnityWebRequest webRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/UpdateSquad.php", form);

        yield return webRequest.SendWebRequest();

        onSquadLoaded?.Invoke(_squad);

        webRequest.Dispose();

        _loadingScreen.gameObject.SetActive(false);
    }
    private IEnumerator AddCharacterRoutine(int index)
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetText("Purchasing Character");

        yield return NetworkUtility.UpdateUserParameter("acoins", 500);
        yield return UpdatePlayerSquad();
    }
}
