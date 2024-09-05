using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
//

public class PlayfabInit : MonoBehaviour
{
    public string customID = "Test1";
    // Start is called before the first frame update
    void Start()
    {

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CreateAccount = false,
            CustomId = customID

        }, LoginResult, OnPlayFabError);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoginResult(LoginResult obj)
    {
        Debug.Log(obj.PlayFabId);

        ProjectManager.instance.playfabID = obj.PlayFabId;

               /* List<string> userData = new List<string> { "Faction", "PlayerTitle", "ATPSuicides", "ATPKills", "ATPDeaths",
             "ATMatchesWon", "ATFactionContribs", "ATXP", "soloMissionIndex", "DidGrantSoloAwards", "isAltAimOn", "DLCSaveState", "DLCAbilityIndex" };

             GetPlayfabData(obj.PlayFabId, userData);

             //get current Credits
             PersistManager.instance.AddPlayfabVC(0);

             GetPlayerProfile(obj.PlayFabId);

             //get user's purchases
             getUserInventory();*/

        //RequestLeaderboard("TopIAA", 20);
         //getCatalogItems();

        

        //StartCoroutine(removeLoader());

    }

    private void OnPlayFabError(PlayFabError obj)
    {
        Debug.Log("OnPlayFabErrorDetails: " + obj.ErrorDetails);
        Debug.Log("OnPlayFabErrorMessage: " + obj.ErrorMessage);
        Debug.Log("OnPlayFabError: " + obj.Error);
        ///TODO: with errors, but still allow people to play
        //goto offline mode

    }

    void GetPlayerData()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
        {

        },
           result =>
           {
               if (result.Inventory == null)
               {
                //retry


               }
               else
               {
                   for (int i = 0; i < result.Inventory.Count; i++)
                   {
                      
                       if (result.Inventory[i].CustomData != null)
                       {

                           var equippedData = result.Inventory[i].CustomData["equipped"];

                           if (equippedData.CompareTo("true") == 0)
                           {
                              
                               /*if (result.Inventory[i].ItemClass.CompareTo("banner") == 0)
                               {
                                   PersistManager.instance.localPlayer.bannerImg = shortName;
                               }*/
                           }

                       }
                       else
                       {
             
                       }
                      

                       if (i >= result.Inventory.Count - 1)
                       {
                        //pull catalog to compare
                        getCatalogItems();
                       }
                   }

                   if (result.Inventory.Count == 0)
                   {
                    //no inventory, so just pull catalog
                    getCatalogItems();
                   }

               }

           },
           error => {
               Debug.Log("Got error retrieving user data:");
            //Debug.Log(error.GenerateErrorReport());
            ///TODO: with errors, but still allow people to play

        });
    }

    void getCatalogItems()
    {
        ProjectManager.instance.inventory.items.Clear();
        int hiddenItemCount = 0;
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {

        },
        result =>
        {
            if (result.Catalog == null)
            {
                //retry
                getCatalogItems();
            }
            else
            {
                Debug.Log("Fetching Catalog " + result.Catalog.Count);
                for (int i = 0; i < result.Catalog.Count; i++)
                {

                    //var catalogItemCustomData = JSON.Parse(result.Catalog[i].DisplayName);
                
                    //var lvl = catalogItemCustomData["level"].Value;

                    string shortName = result.Catalog[i].ItemId;
                    string tag = result.Catalog[i].ItemClass;
                    Debug.Log(shortName + " : " + tag);
                    

                    //
                    InventoryItem item = new InventoryItem();
                    item.displayName = result.Catalog[i].DisplayName;
                    item.rentedTag = result.Catalog[i].ItemClass;

                    ProjectManager.instance.inventory.items.Add(item);
                    
                    int maxCount = (result.Catalog.Count - 1) - hiddenItemCount;
                    

                    if (i >= maxCount)
                    {
                        //done
                    }

                }
            }

        },
        error => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
