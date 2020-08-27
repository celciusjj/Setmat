using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;

public class Navegar : MonoBehaviour
{
    public GameObject go;
    SocketIOComponent socket;

    public void Start()
    {

        socket = go.GetComponent<SocketIOComponent>();



    }



    public void navegarSala()
    {
        SceneManager.LoadScene("Main");
    }

    public void navegarRegistro()
    {
        SceneManager.LoadScene("Registro");
    }

    public void navegarSalaEspera()
    {
        SceneManager.LoadScene("SalaEspera");
    }

    public void navegarJuego()
    {
        SceneManager.LoadScene("Sala");
    }

}
