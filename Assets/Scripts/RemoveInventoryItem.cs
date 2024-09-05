using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;

public class RemoveInventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void removeItem()
    {
        Debug.Log("set item to 0" + gameObject.name);
        PlayFabServerAPI.SetTitleData(
            new SetTitleDataRequest
            {
                Key = gameObject.name,
                Value = "0"
            },
            result =>
            {
                Debug.Log("Set titleData successful");
                gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<GearManager>().ClientGetTitleData();
            },
            error => {
                Debug.Log("Got error setting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );

    }
}
