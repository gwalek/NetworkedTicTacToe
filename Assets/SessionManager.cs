using System;
using System.Threading.Tasks; 
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Multiplayer;
using TMPro;

public class SessionManager : MonoBehaviour
{
    public TMP_InputField JoinCodeInputBox;
    public TextMeshProUGUI JoinCodeDisplayField;
    public GameObject InSessionPannel; 


    async void Start()
    {
        InSessionPannel.SetActive(false);

        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Sign in anonymously succeeded! PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void HostSession()
    {
        await StartSessionAsHost(); 
    }
    public async Task StartSessionAsHost()
    {
        var options = new SessionOptions
        {
            MaxPlayers = 4
        }.WithRelayNetwork(); // or WithDistributedAuthorityNetwork() to use Distributed Authority instead of Relay
        var session = await MultiplayerService.Instance.CreateSessionAsync(options);
        Debug.Log($"Session {session.Id} created! Join code: {session.Code}");
        JoinCodeDisplayField.text = session.Code;
        InSessionPannel.SetActive(true); 


    }

    public async void JoinWithCode()
    {
        await JoinSessionByCode(JoinCodeInputBox.text);
    }
    public async Task JoinSessionByCode(string joinCode)
    {
        var session = await MultiplayerService.Instance.JoinSessionByCodeAsync(joinCode);
        Debug.Log($"Session {session.Id} joined!");
        InSessionPannel.SetActive(true);
    }

 

}
