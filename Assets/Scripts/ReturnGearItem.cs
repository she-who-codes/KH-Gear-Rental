using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;

public class ReturnGearItem : MonoBehaviour
{

    public Text gearTitle; 

    void Start()
    {
        
    }

    public void _ClickReturnGearFromAthlete()
    {
        string newValue = gearTitle.text;
       // Debug.Log("old: " + newValue);
        newValue = newValue.Remove(newValue.LastIndexOf("//RENTED/") + 1);
       // Debug.Log("append item to have child's name:  " + newValue);
        PlayFabServerAPI.SetTitleData(
            new SetTitleDataRequest
            {
                Key = gameObject.name,
                Value = newValue
            },
            result =>
            {
                Debug.Log("Set titleData successful");
                if (gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteProfile>() != null)
                {
                    gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteProfile>().ClientGetTitleData();
                }

            },
            error => {
                Debug.Log("Got error setting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );

    }
}
