using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class InventoryItem
{
    //public INVENTORYCATEGORY category;
    public string displayName;
    public string rentedTag;
   /* public string imageString;
    public string itemClass;
    public int amount;
    public int level;
    public bool hidden = false;*/
   
}
[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "StoreInventory")]
public class StoreInventory : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();
}
