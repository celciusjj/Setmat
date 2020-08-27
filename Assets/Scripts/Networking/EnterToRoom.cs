using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SocketIO;

public class EnterToRoom : MonoBehaviour
{
    public GameObject code, name;
    public GameObject team;

    SocketIOComponent socket;
    //Network cliente;

    void Awake()
    {
        team = GameObject.FindWithTag("team");
    }


    // Start is called before the first frame update
    void Start()
    {


        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("response", (E) =>
        {
            Debug.Log(E.data["team"]);
            if (E.data[1].ToString().Equals("false"))
            {
                SSTools.ShowMessage("La sala no existe o esta llena", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if(E.data[0].ToString().Equals("true"))
            {
                string id = E.data["team"].ToString();
                id = id.Substring(0, id.Length - 1);
                id = id.Substring(1, id.Length - 1);

                if (id.Equals(team.GetComponent<TeamInfo>().id))
                {
                    socket.Emit("callTeams", JSONObject.CreateStringObject(code.GetComponent<UnityEngine.UI.Text>().text));
                    team.GetComponent<TeamInfo>().name = name.GetComponent<UnityEngine.UI.Text>().text;
                    team.GetComponent<TeamInfo>().code = code.GetComponent<UnityEngine.UI.Text>().text;
                    Navegar navegar = new Navegar();
                    navegar.navegarSalaEspera();
                }
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void executeJoinRoom()
    {
        socket.Emit("joinRoom", JSONObject.CreateStringObject(code.GetComponent<UnityEngine.UI.Text>().text+"|"+ team.GetComponent<TeamInfo>().id +"|"+ name.GetComponent<UnityEngine.UI.Text>().text));
    }
}
