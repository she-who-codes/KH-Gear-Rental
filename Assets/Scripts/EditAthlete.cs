using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Drill down button to open a new view that you can see player's profile
public class EditAthlete : MonoBehaviour
{
    public Text playerName;
    public Text playerTeam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _EditPlayer()
    {
        if (gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteManager>() != null)
        {
            gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteManager>()._clickedEditAthlete(playerName.text, playerTeam.text);
        }
    }
}
