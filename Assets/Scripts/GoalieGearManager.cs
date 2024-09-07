using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;
using System;

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
public class GoalieGearManager : MonoBehaviour
{
    string gearType = "Helmet";

    public Text gearSize;
    public Text gearBrand;
    public Text gearColor;

    public Dropdown goalieDropDown;
    GOALIE_GEAR goalieGear;
    

    public Transform gearList;
    public GameObject cellPrefab;

    void OnEnable()
    {
        if (goalieDropDown != null)
        {
            PopulateDropDownWithEnum(goalieDropDown, goalieGear);
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

     
    public void updateGearType(Text type)
    {
        gearType = type.text;

    }


    /// <summary>
    /// Use "Title Data" to log gear Categories //ex:"brand : bauer", "helmet size : 1", "gear type : helmets"
    /// </summary>
    public void SetTitleData()
    {
        int ranID = UnityEngine.Random.Range(100, 9000);
        string key;

        key = "Goalie-" + ranID.ToString();
        
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
        foreach (Transform child in gearList.transform)
        {
            if (gearList.childCount > 0)
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
                    //see if it's already there with same data, do nothing
                    //if it's already there with '0' data, then remove?

                    foreach (var item in result.Data)
                    {

                       // Debug.Log(item.Key + "  " + item.Value);
                        //gearList.a
                        if (item.Value.Length > 2)
                        {
                            if (item.Key.Contains("Goalie") == true)
                            {
                                var item_go = Instantiate(cellPrefab);
                                // do something with the instantiated item -- for instance
                                item_go.GetComponentInChildren<Text>().text = item.Value;
                                item_go.name = item.Key;
                                //parent the item to the content container
                                item_go.transform.SetParent(gearList);
                                //reset the item's scale -- this can get munged with UI prefabs
                                item_go.transform.localScale = Vector2.one;
                                item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);

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
