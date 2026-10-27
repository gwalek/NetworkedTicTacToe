using System;
using System.Threading.Tasks; 
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Multiplayer;
using TMPro;
using System.Xml.Serialization;

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
        SessionOptions options = new SessionOptions
        {
            MaxPlayers = 4
        }.WithRelayNetwork(); 
        // or WithDistributedAuthorityNetwork()
        // to use Distributed Authority instead of Relay
        
        // returning back a class that implents IHostSession interface
        var session = await MultiplayerService.Instance.CreateSessionAsync(options);
        
        
        Debug.Log($"Session {session.Id} created! Join code: {session.Code}");
        
        // Showing Join Code via UI... 
        // TO DO Make it clickable to copy the code 
        JoinCodeDisplayField.text = session.Code;
        InSessionPannel.SetActive(true); 


    }

    public async void JoinWithCode()
    {
        // note, we're not doing any type of validation of the code right now. 
        await JoinSessionByCode(JoinCodeInputBox.text);
        
        // As asuch, it's not dealing with the situations such as...
        // ... the code given is null 
        // ... is not a valid join code 
        // ... the session joined is full 
    }


    public async Task JoinSessionByCode(string joinCode)
    {
        // I should be holding this at Class Member level, not local variable 
        var session = await MultiplayerService.Instance.JoinSessionByCodeAsync(joinCode);
        
        // Joined successful 
        Debug.Log($"Session {session.Id} joined!");
        InSessionPannel.SetActive(true);
    }

    void MethodCallsAsyncFunction()
    {
        SomeAsyncFunction("CallBackMethod");
        // when this function gets called, this method is pretty much is done. 
    }

    // using string to show in text here
    // normally you'd use a delegate in C# for this purpose
    void SomeAsyncFunction(string callbackFunction)
    {
        // Async Operation 
    }

    void CallBackMethod()
    {
        // this is the code the get called when the Async Function ends 
    }

    public async Task NewTAPstyleFunction()
    {
        /// We do stuff to set up
        /// Then call the TAPFunction 
        await TAPfunctionAsync(); 

        // Then continue to write code that functions after it's done
        // this would have been in the call back method. 
        // It's Async code that written and looks synchronous (which is easier) 
    }

    Task TAPfunctionAsync()
    {
        // Async Operation 
        return null; 
    }




}
