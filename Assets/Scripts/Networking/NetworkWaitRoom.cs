using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;


[System.Serializable]
public class TeamCollection
{
    public string team;
    public int score;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}


public class NetworkWaitRoom : MonoBehaviour
{
    public GameObject ranura1, ranura2, ranura3, ranura4, ranura5, ranura6;
    GameObject go, team;
    SocketIOComponent socket;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        team = GameObject.FindWithTag("team");


        socket.Emit("disconnect", JSONObject.CreateStringObject(team.GetComponent<TeamInfo>().name));

        socket.On("onDisconnectTeamResponse", (E) =>
        {
            Debug.Log(E.data.ToString() + "en el desconectar");
            TeamCollection[] equipo = JsonHelper.FromJson<TeamCollection>(E.data.ToString());
            
            llenarCampos(equipo.Length, equipo);
            Navegar navegar = new Navegar();
            navegar.navegarSala();

        });

        socket.On("onStartGame", (E) =>
        {
            StartCoroutine(Camera.main.GetComponent<CountDownController>().CountDownStart());
        });





        socket.On("getTeams", (E) =>
        {
            Debug.Log(E.data.ToString() + "en el get teams");
            TeamCollection[] equipo = JsonHelper.FromJson<TeamCollection>(E.data.ToString());
            llenarCampos(equipo.Length, equipo);
        });

    }

    void llenarCampos(int longitud, TeamCollection[] equipo)
    {
        if(longitud == 0)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura2.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura3.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura4.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura5.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 1)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura3.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura4.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura5.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 2)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = equipo[1].team;
            ranura3.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura4.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura5.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 3)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = equipo[1].team;
            ranura3.GetComponent<UnityEngine.UI.Text>().text = equipo[2].team;
            ranura4.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura5.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 4)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = equipo[1].team;
            ranura3.GetComponent<UnityEngine.UI.Text>().text = equipo[2].team;
            ranura4.GetComponent<UnityEngine.UI.Text>().text = equipo[3].team;
            ranura5.GetComponent<UnityEngine.UI.Text>().text = "";
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 5)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = equipo[1].team;
            ranura3.GetComponent<UnityEngine.UI.Text>().text = equipo[2].team;
            ranura4.GetComponent<UnityEngine.UI.Text>().text = equipo[3].team;
            ranura5.GetComponent<UnityEngine.UI.Text>().text = equipo[4].team;
            ranura6.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        if (longitud == 6)
        {
            ranura1.GetComponent<UnityEngine.UI.Text>().text = equipo[0].team;
            ranura2.GetComponent<UnityEngine.UI.Text>().text = equipo[1].team;
            ranura3.GetComponent<UnityEngine.UI.Text>().text = equipo[2].team;
            ranura4.GetComponent<UnityEngine.UI.Text>().text = equipo[3].team;
            ranura5.GetComponent<UnityEngine.UI.Text>().text = equipo[4].team;
            ranura6.GetComponent<UnityEngine.UI.Text>().text = equipo[5].team;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitTheRoom()
    {
        socket.Emit("onDisconnectTeam", JSONObject.CreateStringObject(team.GetComponent<TeamInfo>().code+"|"+team.GetComponent<TeamInfo>().id));

    }
}
