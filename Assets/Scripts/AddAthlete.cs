using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class AddAthlete : MonoBehaviour
{
    public GameObject manageAthletesUI;
    public Text newPlayerName;
    string playerName;

    string teamName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _updatePlayerName(Text type)
    {
        playerName = type.text;

    }

    public void _updateTeamName(Text type)
    {
        teamName = type.text;

    }
    public void _submitNewAthlete()
    {
        string key = playerName;
        string value = teamName;
        Debug.Log("Add Player: " + key + " : " + value);
       

        PlayFabClientAPI.UpdateUserData(
            // Request
            new PlayFab.ClientModels.UpdateUserDataRequest
            {
                Permission = PlayFab.ClientModels.UserDataPermission.Public,
                Data = new Dictionary<string, string>() { { key, value } }

            },
            // Success
            (PlayFab.ClientModels.UpdateUserDataResult response) =>
            {
                Debug.Log("UpdateUserData completed.");
                //EXIT
                manageAthletesUI.SetActive(true);
                gameObject.SetActive(false);
                //get new catalog and print it ,add to list view and allow deletion
            },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("UpdateUserData failed.");
                Debug.LogError(error.GenerateErrorReport());
            }
            );
        
    }
}
