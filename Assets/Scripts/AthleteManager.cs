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

    public GameObject editAthleteUI;
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

    public void _clickedEditAthlete(string player, string team)
    {
        Debug.Log("Player: " + player);
        editAthleteUI.SetActive(true);
        editAthleteUI.GetComponent<AthleteProfile>().SendData(player, team);
        gameObject.SetActive(false);
    }

    void ClientGetUserTitleData()
    {
        foreach (Transform child in playerList.transform)
        {
            if (playerList.childCount > 0)
            {
                Destroy(child.gameObject);
            }
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = ProjectManager.instance.playfabID,
            Keys = null
        }, result => 
        {

            if (result.Data == null) { Debug.Log("No Key"); }
            else
            {
                string[] getKeys = new string[result.Data.Keys.Count];
                result.Data.Keys.CopyTo(getKeys, 0);
                for (int i = 0; i < getKeys.Length; i++)
                {
                    Debug.Log("Key: " + getKeys[i] + "/ Data: " + result.Data[getKeys[i]].Value);
                    var item_go = Instantiate(cellPrefab);
                    // do something with the instantiated item -- for instance
                    item_go.GetComponentsInChildren<Text>()[0].text = getKeys[i];
                    item_go.GetComponentsInChildren<Text>()[1].text = result.Data[getKeys[i]].Value;
                    item_go.name = getKeys[i];
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
}
