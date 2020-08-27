using UnityEngine;
using UnityEditor;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class RealizarLogin : MonoBehaviour
{
	public GameObject password, email;
	private readonly string basePath = "http://socket-udem.herokuapp.com/";
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
		EditorUtility.DisplayDialog (title, message, "Ok");
#else
        Debug.Log(message);
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/**
	public void postLogin()
	{
		currentRequest = new RequestHelper
		{
			Uri = basePath + "user/logIn ",
			Body = new User
			{

				password = password.GetComponent<UnityEngine.UI.Text>().text,
				email = email.GetComponent<UnityEngine.UI.Text>().text
			},
			//EnableDebug = true
		};
		RestClient.Post<LoginResponse>(currentRequest)
		.Then(res => {

			// And later we can clear the default query string params for all requests
			RestClient.ClearDefaultParams();
			LoginResponse login = JsonUtility.FromJson<LoginResponse>(JsonUtility.ToJson(res));
			Debug.Log(login.message);
		})
		.Catch(err => this.LogMessage("Error", err.Message));


	}
		**/
}
