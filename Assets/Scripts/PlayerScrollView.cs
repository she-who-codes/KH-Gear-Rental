using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScrollView : MonoBehaviour
{
    public Transform playerList;
    public GameObject cellPrefab;
    // Start is called before the first frame update
    void OnEnable()
    {        
        ClientGetTitleData();
    }

    public void ClientGetTitleData()
    {
        PlayFab.PlayFabClientAPI.GetTitleData(new PlayFab.ClientModels.GetTitleDataRequest(),
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
                        item_go.transform.SetParent(playerList);
                        //reset the item's scale -- this can get munged with UI prefabs
                        item_go.transform.localScale = Vector2.one;
                        item_go.transform.localPosition = new Vector3(item_go.transform.position.x, item_go.transform.position.y, 0);
                    }
                    //updatePlayerDataPublisher();

                }
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
}
