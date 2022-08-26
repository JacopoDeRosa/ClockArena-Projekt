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
    [SerializeField] private CharacterCustomizationWindow _equipmentWindow;


    private SquadData _squad;
    private bool _focused;
    private LoadingScreen _loadingScreen;

    public bool SquadLoaded { get => _squad != null; }

    public event Action<int> onCharacterUpdated;
    public event Action<SquadData> onSquadLoaded;
    public event Action<int> onFocus;
    public event Action onLoseFocus;

    private void Awake()
    {
        LoggedUser.onUserLoggedIn += TryGetSquad;
        _loadingScreen = FindObjectOfType<LoadingScreen>(true);
    }

    private void OnDestroy()
    {
        LoggedUser.onUserLoggedIn -= TryGetSquad;
    }

    public SquadData CreateNewSquad(Faction faction, string name)
    {
        SquadData squadData = new SquadData(faction, name);
        _squad = squadData;
        StartCoroutine(UpdateSquadRoutine());
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

    public CharacterComponentsData GetCharacterData(int index)
    {
        return _squad.characters[index];
    }

    public void UpdateCharacterData(int index, CharacterComponentsData data)
    {
        _squad.characters[index] = data;
        onCharacterUpdated?.Invoke(index);
    }

    public void SpawnCharacterAtIndex(int index, Action<Character> callback)
    {
        if (index >= _characterBases.Length) return;
        StartCoroutine(AddCharacterRoutine(index, callback));
    }

    public void FocusOnCharacter(int index)
    {
        if (index >= _characters.Length) return;
        if (_focused) return;
        if (_characters[index] == null) return;
        
        _inspectionCamera.transform.rotation = Quaternion.identity;
        _inspectionCamera.transform.position = _characterBases[index].position;
        _inspectionCamera.gameObject.SetActive(true);
        _focused = true;
        onFocus?.Invoke(index);
    }
    public void LooseFocus()
    {
        if (_focused == false) return;
        _focused = false;
        _inspectionCamera.SetActive(false);
        onLoseFocus?.Invoke();
    }
    public IEnumerator UpdateSquad()
    {

        string squadJson = JsonUtility.ToJson(_squad);

        WWWForm form = NetworkUtility.GetSignedForm();

        form.AddField("owner", LoggedUser.UserData.userName);
        form.AddField("squad", squadJson);

        UnityWebRequest webRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/UpdateSquad.php", form);

        yield return webRequest.SendWebRequest();

        onSquadLoaded?.Invoke(_squad);

        webRequest.Dispose();

    }

    private void TryGetSquad(UserData userData)
    {
        StartCoroutine(LoadSquadRoutine(userData.userName));
    }

    private IEnumerator LoadSquadRoutine(string owner)
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
                    continue;
                }

                Character spawnedChar = Instantiate(_characterTemplate, _characterBases[i]);
                _characters[i] = spawnedChar;
                spawnedChar.DataReader.ReadData(squad.GetCharacterAtIndex(i));
                onCharacterUpdated?.Invoke(i);
            }
           

            _squad = squad;
        }

        onSquadLoaded?.Invoke(squad);

        webRequest.Dispose();

        _loadingScreen.gameObject.SetActive(false);
    }
    private IEnumerator UpdateSquadRoutine()
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
        yield return UpdateSquad();

        _loadingScreen.gameObject.SetActive(false);
    } 
    private IEnumerator AddCharacterRoutine(int index, Action<Character> callback)
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetText("Purchasing Character");

        void PlaceCharacter(bool valid)
        {
            if (valid)
            {
                CharacterComponentsData componentsData = _squad.AddNewCharacterAtIndex(index);
                Character spawnedChar = Instantiate(_characterTemplate, _characterBases[index]);
                _characters[index] = spawnedChar;
                spawnedChar.DataReader.ReadData(componentsData);
                callback(spawnedChar);
            }
        }

        yield return NetworkUtility.TrySpendACoins(100, PlaceCharacter);

        _loadingScreen.SetText("Updating Squad");

        yield return UpdateSquad();

        _loadingScreen.gameObject.SetActive(false);
    }
}
