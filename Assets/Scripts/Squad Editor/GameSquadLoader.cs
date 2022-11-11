using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSquadLoader : MonoBehaviour
{
    [SerializeField] private Character _characterTemplate;
    [SerializeField] private Transform[] _spawnPoints;

    private GameTurnManager _turnManager;

    private void Awake()
    {
        _turnManager = FindObjectOfType<GameTurnManager>();
    }

    private void Start()
    {
        if (LoggedUser.IsLogged)
        {
            StartCoroutine(LoadSquadRoutine(LoggedUser.UserData.userName, true));
        }
        else
        {
            _turnManager.BeginNewTurn();
        }
    }

    private IEnumerator LoadSquadRoutine(string owner, bool friendlySquad)
    {
        WWWForm form = NetworkUtility.GetSignedForm();

        form.AddField("owner", owner);


        UnityWebRequest webRequest = UnityWebRequest.Post(NetworkUtility.dbUrl + "/LoadUserSquad.php", form);

        yield return webRequest.SendWebRequest();

        SquadData squad = null;

        if (webRequest.downloadHandler.text == "3")
        {
            Debug.LogError("User has no squad assigned");
        }
        else if (webRequest.downloadHandler.text == "4")
        {
            Debug.LogError("Squad Query Error");
        }
        else
        {
            squad = JsonUtility.FromJson<SquadData>(webRequest.downloadHandler.text);
        }

        if (webRequest.error != null)
        {
            Debug.LogError("Server error in squad editor: " + webRequest.error);
        }

        if (squad != null)
        {
            List<Character> spawnedCharacters = new List<Character>();

            for (int i = 0; i < squad.characters.Length; i++)
            {
                if (squad.characters[i].available == false)
                {
                    continue;
                } 

                Character spawnedChar = Instantiate(_characterTemplate, _spawnPoints[i].position, _spawnPoints[i].rotation);
                spawnedCharacters.Add(spawnedChar);
                spawnedChar.DataReader.ReadData(squad.GetCharacterAtIndex(i));
                spawnedChar.GUI.ShowGui(true);
                spawnedChar.SetIsPlayer(spawnedChar);
            }

            _turnManager.AddCharacters(spawnedCharacters);
            _turnManager.BeginNewTurn();
        }

        webRequest.Dispose();

    }
}
