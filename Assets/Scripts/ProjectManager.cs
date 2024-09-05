using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

[System.Serializable]
public class GearItem
{
    public string name;
    public string value;

}
public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;
    public StoreInventory inventory;

    public string playfabID;

    //public Dictionary<string, string> gearList = new Dictionary<string, string>();
    public List<GearItem> gearList = new List<GearItem>();



    private void SingletonFunction()
    {
        if (ProjectManager.instance == null)
        {
            ProjectManager.instance = this;

            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Awake()
    {
        SingletonFunction();
    }

    void Start()
    {
    }

    public void addGearItem(string key, string value)
    {
        GearItem item = new GearItem();
        item.name = key;
        item.value = value;

        /*if (gearList.Contains(item))
        {
            if (gearList[item].value.CompareTo(value) != 0)
            {
                gearList[item].value = value;
            }
            //string oldValue = gearList[item].value;
            if (oldValue.CompareTo(value) != 0)
            {
                oldValue = value;
            }
        }
        else
        {           

            gearList.Add(item);
        }*/

        //ProjectManager.instance.gearList.Add(item.Key, item.Value);
        /*foreach (KeyValuePair<string, string> entry in gearList)
            {

            }*/
    }

    //grant an item to a player when rented
    //revoke the item when returned
    //list of all players, team (sorting)
    //list of all gear category, size, brand, available

    //add players

    //add gear

    //coaches can view kid & gear assigned
    //one admin for adjusting gear, adjusting players
    //August 9th doing inventory

    /*
    * Use "Title Data" to log gear Categories //ex:"brand : bauer", "helmet size : 1", "gear type : helmets"
    * use "Player Data Client" to log specific ones in the inventory //ex: "uid : 1 small bauer helmet" (log with unique identifiers)
    * use "Player Data Client" to log kids //ex: "ukid : Joel Lowry / Stingrays" (allow for search)
    * Use "Player Data Publisher" to log Kids with specific checked out gear // ex: "Joel Lowry / Stingrays: small bauer helmet / large knee pads / 18" goalie stick"
    * Deleting Data: reset value to a key to '0'. When pulling data, check if value == 0, if true then don't display that line item.
    * Setting Data: check for keys first that == '0', if true, set those first, if not found, then create a new entry
     */
}
