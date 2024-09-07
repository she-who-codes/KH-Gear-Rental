using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class AthleteManager : MonoBehaviour
{
    //list of all players in the system, with edit button to drill down
    public Transform playerList;
    public GameObject cellPrefab;

    public Text newPlayerName;
    string playerName;
    //create a person
    //assign/revoke rental goalie gear
    //assign/revoke rental skater gear
    //assign/revoke a team

    //scroll view of gear they have assigned to them
    //

    void OnEnable()
    {
        ClientGetUserTitleData();
    }

    public void _updatePlayerName(Text type)
    {
        playerName = type.text;

    }

    //UpdatePlayerDataTitle
    public void _submitNewAthlete()
    {
        string key = playerName;
        string value = playerName;

        foreach (Transform child in playerList.transform)
        {
            if (playerList.childCount > 0)
            {
                Destroy(child.gameObject);
            }
        }

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
                //get new catalog and print it ,add to list view and allow deletion
            },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("UpdateUserData failed.");
                Debug.LogError(error.GenerateErrorReport());
            }
            );

        ClientGetUserTitleData();
    }

    void ClientGetUserTitleData()
    {

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = ProjectManager.instance.playfabID,
            Keys = null
        }, result => 
        {

            if (result.Data == null) { Debug.Log("No Key"); }
            else
            {

                foreach (var item in result.Data)
                {

                    Debug.Log(item.Key + "  " + item.Value);
                    var item_go = Instantiate(cellPrefab);
                    // do something with the instantiated item -- for instance
                    item_go.GetComponentInChildren<Text>().text = item.Key;
                    item_go.name = item.Key;
                    //parent the item to the content container
                    item_go.transform.SetParent(playerList);
                    //reset the item's scale -- this can get munged with UI prefabs
                    item_go.transform.localScale = Vector2.one;
                    item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);
                }
            }

            
        }, (error) =>
        {
            Debug.Log("Got error retrieving user data:");
        });

    }

    //called from the edit ont he scroll view
    public void OpenProfile()
    {

    }
}
