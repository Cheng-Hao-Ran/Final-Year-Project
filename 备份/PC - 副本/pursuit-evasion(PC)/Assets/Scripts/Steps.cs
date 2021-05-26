using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Steps : MonoBehaviour
{
    public static Steps step;
    public Mapunit[,] array;
    
    public Steps() {
        step = this;
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("random_map_test", LoadSceneMode.Single);
    }
    public void Active()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        
    }

    public void Inactive()
    {
        this.gameObject.SetActive(false);
        Data.robber.fly = true;
    }

    public void Freeze()
    {
        Data.cop.step=1;
        Data.cop1.step = 1;
        this.gameObject.GetComponent<Button>().interactable = false;
    }

    
    public void announceFreeze()
    {
        GameObject root = GameObject.Find("Main Camera");
        GameObject obj = root.transform.Find("Announcement").gameObject;
        obj.SetActive(true);
        
        string str = "Cops are all freezed!They can only move 1 unit in this turn.";
        GameObject.Find("Main Camera/Announcement/Image/Notes").GetComponent<Text>().text = str;
        
    }

    public void announceEasy()
    {
        GameObject root = GameObject.Find("Main Camera");
        GameObject obj = root.transform.Find("Announcement").gameObject;
        obj.SetActive(true);

        string str = "It uses A-star algorithm and cops can move 4 steps each turn.";
        GameObject.Find("Main Camera/Announcement/Image/Notes").GetComponent<Text>().text = str;
    }

    public void announceHard()
    {
        GameObject root = GameObject.Find("Main Camera");
        GameObject obj = root.transform.Find("Announcement").gameObject;
        obj.SetActive(true);

        string str = "It uses minimax algorithm and cops can move 5 steps each turn.";
        GameObject.Find("Main Camera/Announcement/Image/Notes").GetComponent<Text>().text = str;
    }
    public void minimax()
    {
        Data.cop1.difficult = 1;
        Data.cop.difficult = 1;
        Data.cop1.step = 5;
        Data.cop.step = 5;
    }

    public void announceTrick()
    {
        GameObject root = GameObject.Find("Main Camera");
        GameObject obj = root.transform.Find("Announcement").gameObject;
        obj.SetActive(true);

        string str = "You leave a substitute and Cops will chase it in this turn.";
        GameObject.Find("Main Camera/Announcement/Image/Notes").GetComponent<Text>().text = str;
    }
    public void Teleport()
    {
        
         Mapunit teleport1 = array[Data.robber.robberZ, Data.robber.robberX];
         Mapunit teleport2 = array[Data.robber.robberZ, Data.robber.robberX].getTeleport();
         Data.robber.transform.position = new Vector3(teleport2.getX() * 5, 3, teleport2.getZ() * 5);
         Data.robber.robberX = teleport2.getX();
         Data.robber.robberZ = teleport2.getZ();
         if (teleport1.getType() == 4)
         {
            teleport1.setType(1);
            GameObject.Find("Cube" + teleport1.getNumber()).GetComponent<MeshRenderer>().material.color = Color.blue;
        }
         else if(teleport1.getType()==5)
         {
            teleport1.setType(3);
            GameObject.Find("Cube" + teleport1.getNumber()).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        

        if (teleport2.getType() == 4)
        {
            teleport2.setType(1);
            GameObject.Find("Cube" + teleport2.getNumber()).GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (teleport2.getType() == 5)
        {
            teleport2.setType(3);
            GameObject.Find("Cube" + teleport2.getNumber()).GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        Data.robber.fly = true;
        this.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        array = RandomMap.array;

    }

// Update is called once per frame
    void Update()
    {
        
    }
}
