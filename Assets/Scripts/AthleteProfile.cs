using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AthleteProfile : MonoBehaviour
{
    public Text playerName;
    public Dropdown teamDropdown;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log(option.text);
        }
        
        Debug.Log(name + team);
    }
     
}
