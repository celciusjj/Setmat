using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryOption : MonoBehaviour
{
    public bool status;
    public string body;
    public GameObject parent;
    public GameObject networkRoom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<UnityEngine.UI.Text>().text != body)
        {
            this.GetComponent<UnityEngine.UI.Text>().text = body;
        }
    }

    public void answer()
    {
        NetworkRoom other = (NetworkRoom) networkRoom.GetComponent(typeof(NetworkRoom));
        if (other.intentos == 0)
        {
            if (status && !body.Equals(""))
            {
                other.emitResponse("correcto");
                other.intentos++;
                parent.GetComponent<Image>().color = Color.green;
            }
            else if(!body.Equals(""))
            {
                other.intentos++;
                other.emitResponse("incorrecto");
                parent.GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            Debug.Log("se acabaron los intentos");
        }
    }
}
