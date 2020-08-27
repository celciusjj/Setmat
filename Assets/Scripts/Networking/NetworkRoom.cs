
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

[System.Serializable]
public class Category
{
    public string body;
    public bool status;
}


public static class JsonHelperTwo
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

public class NetworkRoom : MonoBehaviour
{
    public int countdownTime;
    public GameObject go, team;
    public SocketIOComponent socket;
    public Text category1, category2, category3, category4, concept;
    string respuestaCorrecta, codigoSiguienteEquipo;
    public int intentos = 0;
    public GameObject btnCategoriaUno, btnCategoriaDos, btnCategoriaTres, btnCategoriaCuatro;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        team = GameObject.FindWithTag("team");
        btnCategoriaUno.active = false;
        btnCategoriaDos.active = false;
        btnCategoriaTres.active = false;
        btnCategoriaCuatro.active = false;
        concept.GetComponent<UnityEngine.UI.Text>().text = "Espera tu turno...";

        /**
         * Se ejecuta cuando finaliza la partida
         */
        socket.On("gameOver", (E) =>
        {
            IEnumerator coroutine = gameOver();
            StartCoroutine(coroutine);
        });

        socket.On("sendQuestion", (E) =>
        {

            setDefaultValues();
            //Debug.Log(E.data + "en sendquestion");
            string currentTeam = E.data["currentTeam"].ToString();
            currentTeam = currentTeam.Substring(0, currentTeam.Length - 1);
            currentTeam = currentTeam.Substring(1, currentTeam.Length - 1);

            if (currentTeam.Equals(team.GetComponent<TeamInfo>().id))
            {
                Category[] categories = JsonHelperTwo.FromJson<Category>(E.data[0].ToString());
                category1.GetComponent<CategoryOption>().body = categories[0].body;
                category1.GetComponent<CategoryOption>().status = categories[0].status;
                category2.GetComponent<CategoryOption>().body = categories[1].body;
                category2.GetComponent<CategoryOption>().status = categories[1].status;
                category3.GetComponent<CategoryOption>().body = categories[2].body;
                category3.GetComponent<CategoryOption>().status = categories[2].status;
                category4.GetComponent<CategoryOption>().body = categories[3].body;
                category4.GetComponent<CategoryOption>().status = categories[3].status;
                concept.GetComponent<UnityEngine.UI.Text>().text = E.data[2]["concept"].ToString();


                for (int i = 0; i < categories.Length; i++)
                {
                    if (categories[i].status)
                    {
                        respuestaCorrecta = categories[i].body;
                    }
                }
            }
            else
            {
                category1.GetComponent<CategoryOption>().body = "";
                category1.GetComponent<CategoryOption>().status = false;
                category2.GetComponent<CategoryOption>().body = "";
                category2.GetComponent<CategoryOption>().status = false;
                category3.GetComponent<CategoryOption>().body = "";
                category3.GetComponent<CategoryOption>().status = false;
                category4.GetComponent<CategoryOption>().body = "";
                category4.GetComponent<CategoryOption>().status = false;
                concept.GetComponent<UnityEngine.UI.Text>().text = "Espera tu turno...";
                btnCategoriaUno.active = false;
                btnCategoriaDos.active = false;
                btnCategoriaTres.active = false;
                btnCategoriaCuatro.active = false;
            }
            
        }); 
    }

    public IEnumerator gameOver()
    {
        concept.text = "Juego terminado";
        //countdownDiplay.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        //displayRoom.gameObject.SetActive(false);
        Navegar navegar = new Navegar();
        navegar.navegarSala();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setDefaultValues()
    {
        btnCategoriaUno.active = true;
        btnCategoriaDos.active = true;
        btnCategoriaTres.active = true;
        btnCategoriaCuatro.active = true;
        intentos = 0;
        btnCategoriaUno.GetComponent<Image>().color = Color.white;
        btnCategoriaDos.GetComponent<Image>().color = Color.white;
        btnCategoriaTres.GetComponent<Image>().color = Color.white;
        btnCategoriaCuatro.GetComponent<Image>().color = Color.white;
    }

    public void emitResponse(string resultado)
    {
        if (resultado.Equals("correcto"))
        {
            team.GetComponent<TeamInfo>().score++;
            socket.Emit("getScore", JSONObject.CreateStringObject(team.GetComponent<TeamInfo>().code + "|" + team.GetComponent<TeamInfo>().id + "|" + team.GetComponent<TeamInfo>().score));
        }
        else if(resultado.Equals("incorrecto"))
        {
            socket.Emit("getScore", JSONObject.CreateStringObject(team.GetComponent<TeamInfo>().code + "|" + team.GetComponent<TeamInfo>().id + "|" + team.GetComponent<TeamInfo>().score));
        }
        else
        {
            Debug.Log("ya no puede responder");
        }

    }


}
