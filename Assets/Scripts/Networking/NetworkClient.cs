using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
//using Project.Utility;
using UnityEngine.UI;


namespace Project.Networking
{


    public class NetworkClient : SocketIOComponent
    {
        public GameObject code, name;
        //[Header("Network Client")]
        //[SerializeField]
        //private Transfrom networkContainer;

        // Start is called before the first frame update
        public override void  Start()
        {
            base.Start();
            setupEvents();

        }



        // Update is called once per frame
        public override void  Update()
        {
            base.Update();
        }

    

        public void setupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("conection made to the server");
            });

            On("response", (E) =>
            {
                Navegar navegar = new Navegar();
                navegar.navegarSalaEspera();
            });

            Emit("disconnect", (E) =>
            {
                Debug.Log("desconectado");
            });

            getTeams();
        }

        public void joinRoom()
        {
           // Debug.Log(JSONObject.CreateStringObject(value));
            //Emit("joinRoom", JSONObject.CreateStringObject(JsonUtility.ToJson(team)));
            Emit("joinRoom", JSONObject.CreateStringObject(code.GetComponent<UnityEngine.UI.Text>().text+"|"+name.GetComponent<UnityEngine.UI.Text>().text));
        }

        public void callTeam()
        {
            Emit("callTeams", (E) => 
            { 
            
            });
        }

        void getTeams()
        {
            On("getTeams", (E) =>
            {
                Debug.Log(E.data);
            });
        }
    }
}
