using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour
{
    GameObject go;
    SocketIOComponent socket;
    public GameObject team;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        setupEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setupEvents()
    {
        socket.On("open", (E) =>
        {
            Debug.Log("conection made to the server");
        });

        socket.On("connected", (E) =>
        {
            team = GameObject.FindWithTag("team");
            string id = E.data["id"].ToString();
            id = id.Substring(0, id.Length - 1);
            id = id.Substring(1, id.Length - 1);
            team.GetComponent<TeamInfo>().id = id;
        });

        socket.Emit("disconnect", (E) =>
        {
            Debug.Log("desconectado");
        });

    }
}
