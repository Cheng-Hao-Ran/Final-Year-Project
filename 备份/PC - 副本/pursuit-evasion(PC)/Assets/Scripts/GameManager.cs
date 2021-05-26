using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int turns = 10;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        Data.gameManager = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Data.robber.step > 0)
        {
            Data.robber.state = true;
            Data.robber.turn = true;
            string str = "Steps left: " + Data.robber.step;
            GameObject.Find("Main Camera/Information/Text").GetComponent<Text>().text = str;
            GameObject.Find("Main Camera/Information/Step").SetActive(false);
            GameObject.Find("Main Camera/Information/Skills").SetActive(false);
            Debug.Log("rob 10");

            Data.robber.Move();
        }
        else if (Data.robber.step == 0)
        {
            Debug.Log("rob 11");

            Data.robber.timer = 0;
            Data.robber.startSuc = 1;
            Data.robber.teleport();
            if (Data.robber.turn == true &&Data.robber.fly==true)
            {
                Data.cop.suc = 1;

            }

            //GameObject.Find("Main Camera/Information/Button").SetActive(true);

        }
        if (Data.cop.suc == 1)
        {
            Data.cop.move(0);
        }
        if (Data.cop1.suc == 1)
        {
            Data.cop1.move(1);
        }
        if (Data.gameManager.turns <= 0 && Data.gameManager.state==0)
        {

            GameObject root = GameObject.Find("Main Camera");
            GameObject obj = root.transform.Find("End").gameObject;
            obj.SetActive(true);

            Debug.Log(obj);
            GameObject.Find("Main Camera/Information").SetActive(false);
            //GameObject.Find("Main Camera").transform.Find("Main Camera/End").gameObject.SetActive(true);
            //GameObject.Find("Main Camera/End").SetActive(true);
            //obj.GetComponent("Text").GetComponent<Text>().text = "You win!";
            GameObject.Find("Main Camera/Options/Text").GetComponent<Text>().text ="You win!";
            Data.gameManager.state = 1;
        }
    }  
}
