using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AthleteViewGearOnlyPanel : MonoBehaviour
{ 
    public Text athleteNameText;


    public Transform gearList;
    public GameObject viewGearOnlyCellPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void setAthleteName(string name)
    {
        athleteNameText.text = name;
    }
    void OnEnable()
    {
        ClientGetTitleData();
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
                    foreach (var item in result.Data)
                    {
                        if (item.Value.Length > 2)
                        {
                            if (item.Value.Contains("RENTED") == true)
                            {
                                //check if user name matches to rented gear
                                if (item.Value.Contains(athleteNameText.text) == true)
                                {
                                    var item_go = Instantiate(viewGearOnlyCellPrefab);

                                    string gearItem = item.Value;
                                    gearItem = gearItem.Remove(gearItem.LastIndexOf("/RENTED/") + 1);
                                    item_go.GetComponentInChildren<Text>().text = gearItem;
                                    item_go.name = item.Key;
                                    //parent the item to the content container
                                    item_go.transform.SetParent(gearList);

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
