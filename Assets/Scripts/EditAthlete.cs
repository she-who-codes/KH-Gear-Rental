using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Drill down button to open a new view that you can see player's profile
public class EditAthlete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _EditPlayer()
    {
        if (gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteManager>() != null)
        {
            gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteManager>().OpenProfile();
        }
    }
}
