using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Proyecto26;
using UnityEditor;
using UnityEngine.UI;

public class Registrarse : MonoBehaviour
{
	public GameObject name, password, email;

    private readonly string basePath = "https://socket-udem.herokuapp.com";
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
		Debug.Log("ejecuta");
		//postRegistro();
	}

    // Update is called once per frame
    void Update()
    {

	}

	/**
	public void postRegistro()
	{
		
		currentRequest = new RequestHelper
		{
			Uri = basePath + "/user/createUser",
			Body = new User
			{
				name = name.GetComponent<UnityEngine.UI.Text>().text,
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
