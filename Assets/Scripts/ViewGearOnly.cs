using UnityEngine;
using UnityEngine.UI;

public class ViewGearOnly : MonoBehaviour
{

    public Text playerName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void _ClickViewGear()
    {
        if (gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteViewer>() != null)
        {
            gameObject.transform.parent.transform.parent.transform.parent.GetComponentInParent<AthleteViewer>()._clickedViewAthleteGear(playerName.text);
        }
    }

}
