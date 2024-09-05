using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminLogIn : MonoBehaviour
{
    public string tempPin = "2334";
    public GameObject adminPanel;
    public Text pincodeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void _enterPinCode(Text enteredCode)
    {
        if (enteredCode.text.CompareTo(tempPin) == 0)
        {
            adminPanel.SetActive(true);
        }
    }
}
