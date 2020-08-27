using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDiplay;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CountDownStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CountDownStart()
    {
        countdownDiplay.fontSize = 60;
        //countdownDiplay.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            countdownDiplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDiplay.gameObject.SetActive(false);
        Navegar navegar = new Navegar();
        navegar.navegarJuego();
    }
}
