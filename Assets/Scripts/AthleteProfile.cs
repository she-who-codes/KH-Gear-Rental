using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AthleteProfile : MonoBehaviour
{
    public Text playerName;
    public Dropdown teamDropdown;

    public GameObject rentGearUI;

    public Transform rentedGearList;
    public GameObject returnableCellPrefab;

    public Transform skaterGearList;
    public GameObject skaterGearCellPrefab;

    public Transform goalieGearList;
    public GameObject goalieGearCellPrefab;

    string teamName;

    void OnEnable()
    {
        ClientGetTitleData();
        //show player's data
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void _updateTeamName(Text type)
    {
        teamName = type.text;
    }

    public void _ClickUpdateTeam()
    {
        //send team to player's profile
        //Debug.Log(playerName.text + " : " + teamName);
        string key = playerName.text;
        string value = teamName;
        //Debug.Log("Add Player: " + key + " : " + value);


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
    }

    public void SendData(string name, string team)
    {
        playerName.text = name;

        int i = 0;

        foreach (Dropdown.OptionData option in teamDropdown.options)
        {
            if (option.text.CompareTo(team) == 0)
            {
                teamDropdown.value = i;
                break;
            }
            i++;
            //Debug.Log(option.text);
        }

        //Debug.Log(name + team);
    }

    public void ClientGetTitleData()
    {
        foreach (Transform child in goalieGearList.transform)
        {
            if (goalieGearList.childCount > 0)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Transform child in skaterGearList.transform)
        {
            if (skaterGearList.childCount > 0)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Transform child in rentedGearList.transform)
        {
            if (rentedGearList.childCount > 0)
            {
                Destroy(child.gameObject);
            }
        }

        PlayFabClientAPI.GetTitleData(new PlayFab.ClientModels.GetTitleDataRequest(),
            result => {
                if (result.Data == null)
                {
                }
                else
                {
                    foreach (var item in result.Data)
                    {
                        if (item.Value.Length > 2)
                        {
                            if (item.Value.Contains("RENTED") == false)
                            {

                                if (item.Key.Contains("Goalie") == true)
                                {
                                    var item_go = Instantiate(goalieGearCellPrefab);
                                    // do something with the instantiated item -- for instance
                                    item_go.GetComponentInChildren<Text>().text = item.Value;
                                    item_go.name = item.Key;
                                    //parent the item to the content container
                                    item_go.transform.SetParent(goalieGearList);

                                    item_go.GetComponent<RentGearItem>().athleteName = playerName.text;
                                    //reset the item's scale -- this can get munged with UI prefabs
                                    item_go.transform.localScale = Vector2.one;
                                    item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);

                                }

                                if (item.Key.Contains("Skater") == true)
                                {
                                    var item_go = Instantiate(skaterGearCellPrefab);
                                    // do something with the instantiated item -- for instance
                                    item_go.GetComponentInChildren<Text>().text = item.Value;
                                    item_go.name = item.Key;
                                    //parent the item to the content container
                                    item_go.transform.SetParent(skaterGearList);

                                    item_go.GetComponent<RentGearItem>().athleteName = playerName.text;
                                    //reset the item's scale -- this can get munged with UI prefabs
                                    item_go.transform.localScale = Vector2.one;
                                    item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);

                                }
                            }
                            else //is rented, so check if user name matches
                            {
                                if (item.Value.Contains(playerName.text) == true)
                                {
                                        var item_go = Instantiate(returnableCellPrefab);
                                        // do something with the instantiated item -- for instance
                                        item_go.GetComponentInChildren<Text>().text = item.Value;
                                        item_go.name = item.Key;
                                        //parent the item to the content container
                                        item_go.transform.SetParent(rentedGearList);

                                        //item_go.GetComponent<ReturnGearItem>().athleteName = playerName.text;
                                        //reset the item's scale -- this can get munged with UI prefabs
                                        item_go.transform.localScale = Vector2.one;
                                        item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);

                                    
                                }
                            }
                        }
                    }

                }
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
     

}
