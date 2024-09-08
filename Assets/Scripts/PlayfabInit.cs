using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabInit : MonoBehaviour
{
    public string customID = "Test1";

    void Start()
    {

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CreateAccount = false,
            CustomId = customID

        }, LoginResult, OnPlayFabError);
    }


    private void LoginResult(LoginResult obj)
    {
        Debug.Log(obj.PlayFabId);

        ProjectManager.instance.playfabID = obj.PlayFabId;
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        Debug.Log("OnPlayFabErrorDetails: " + obj.ErrorDetails);
        Debug.Log("OnPlayFabErrorMessage: " + obj.ErrorMessage);
        Debug.Log("OnPlayFabError: " + obj.Error);

    }
}
