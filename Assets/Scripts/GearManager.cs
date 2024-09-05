using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;
using System;

[System.Serializable]
public enum SKATER_GEAR
{
    Helmet,
    Gloves,
    Shin_Pads,
    Elbow_Pads,
    Stick,
    Skates
}

[System.Serializable]
public enum GOALIE_GEAR
{
    Helmet,
    Chest_Pad,
    Stick,
    Shin_Pads,
    Neck_Guard,
    Catcher_Glove,
    Blocker_Glove,
    Breezers,
    Goalie_CA
}
public class GearManager : MonoBehaviour
{
    string gearType;

    public Text gearSize;
    public Text gearBrand;
    public Text gearColor;

    public Dropdown goalieDropDown;
    GOALIE_GEAR goalieGear;

    public Dropdown skaterDropDown;
    SKATER_GEAR skaterGear;

    public Transform gearList;
    public GameObject cellPrefab;

    void OnEnable()
    {
        if (goalieDropDown != null)
        {
            PopulateDropDownWithEnum(goalieDropDown, goalieGear);
        }
        if (skaterDropDown != null)
        {
            PopulateDropDownWithEnum(skaterDropDown, skaterGear);
        }
        ClientGetTitleData();
    }

    void Start()
    {
       
    }
       

    public static void PopulateDropDownWithEnum(Dropdown dropdown, Enum targetEnum)//You can populate any dropdown with any enum with this method
    {
        Type enumType = targetEnum.GetType();//Type of enum(FormatPresetType in my example)
        List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();

        for (int i = 0; i < Enum.GetNames(enumType).Length; i++)//Populate new Options
        {
            newOptions.Add(new Dropdown.OptionData(Enum.GetName(enumType, i)));
        }

        dropdown.ClearOptions();//Clear old options
        dropdown.AddOptions(newOptions);//Add new options
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropdownValueChanged(Text change)
    {
        gearType = "New Value : " + change.text;
    }
    public void updateGearType(Text type)
    {
        gearType = type.text;
        
    }
    
    //UpdatePlayerDataTitle
    public void SubmitToPlayfab()
    {
        
        int ranID = UnityEngine.Random.Range(100, 9000);
        string key = ranID.ToString();
        string value = gearType + "/" + gearSize + "/" + gearBrand;
        //JSONArray array = jsonNode.as;
        PlayFabClientAPI.UpdateUserData(
            // Request
            new PlayFab.ClientModels.UpdateUserDataRequest
            {
                Permission = PlayFab.ClientModels.UserDataPermission.Public,
                 Data = new Dictionary<string, string>() {{key, value}}
           
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
        SetTitleData();

    }


    /// <summary>
    /// Use "Title Data" to log gear Categories //ex:"brand : bauer", "helmet size : 1", "gear type : helmets"
    /// </summary>
    public void SetTitleData()
    {
        int ranID = UnityEngine.Random.Range(100, 9000);
        string key;
        if (goalieDropDown != null)
        {
            key = "Goalie-" + ranID.ToString();
        }
        else
        {
            key = "Skater-" + ranID.ToString();
        }


        string value = gearType + "/" + gearSize.text + "/" + gearBrand.text + "/" + gearColor.text;

        PlayFabServerAPI.SetTitleData(
            new SetTitleDataRequest
            {
                Key = key,
                Value = value
            },
            result => 
            {
                Debug.Log("Set titleData successful");
                ClientGetTitleData();
            },
            error => {
                Debug.Log("Got error setting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new PlayFab.ClientModels.GetTitleDataRequest(),
            result => {
                if (result.Data == null)
                {
                } 
                else
                {
                    foreach (var item in result.Data)
                    {
                        Debug.Log(item.Key + "  " + item.Value);
                        //gearList.a
                        var item_go = Instantiate(cellPrefab);
                        // do something with the instantiated item -- for instance
                        item_go.GetComponentInChildren<Text>().text = item.Value;
                        //parent the item to the content container
                        item_go.transform.SetParent(gearList);
                        //reset the item's scale -- this can get munged with UI prefabs
                        item_go.transform.localScale = Vector2.one;
                        item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);
                    }
                    updatePlayerDataPublisher();
                    // Debug.Log("MonsterName: " + result.Data["MonsterName"]);

                }
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    void updatePlayerDataPublisher()
    {

        PlayFabClientAPI.UpdateUserPublisherData(
           // Request
           new PlayFab.ClientModels.UpdateUserDataRequest
           {
               Permission = PlayFab.ClientModels.UserDataPermission.Public,
               Data = new Dictionary<string, string>() { { "test 1", " test" } }
               //Data = new Dictionary<string, string>() { { "GearType", gearType }, { "GearSize", gearSize }, { "GearBrand", gearBrand } }

           },
           // Success
           (PlayFab.ClientModels.UpdateUserDataResult response) =>
           {
               Debug.Log("UpdateUserData completed.");
               ClientGetUserPublisherData();
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
   

    public void ClientGetUserPublisherData()
    {
        PlayFabClientAPI.GetUserPublisherData(new PlayFab.ClientModels.GetUserDataRequest()
        {
            PlayFabId = ProjectManager.instance.playfabID
        }, result => {
            if (result.Data == null) { Debug.Log("No SomeKey"); }
            else
            {
                foreach (var item in result.Data)
                {
                    Debug.Log(item.Key + "  " + item.Value);
                }
            }
        },
        error => {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
