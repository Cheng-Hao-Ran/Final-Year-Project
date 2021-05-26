using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cop : MonoBehaviour
{
    public Mapunit[,] array;
    public int suc = 0;
    public int copX = 1;
    public int copZ = 0;
    public int step = 4;
    public int index = 0;
    public int unit = 5;
    public int timer = 0;
    public int difficult=0;
    
    public bool pathFind = false;
    public List<Mapunit> roadCube = new List<Mapunit>();
    List<Mapunit> path;
    List<Color> colorSet = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {

        initialized(0);
    }
    void FixedUpdate()
    {

    }


    public void initialized(int n)
    {
        array = RandomMap.array;
        if (n == 0)
        {
            Data.cop = this;
        }
        if (n == 1)
        {
            Data.cop1 = this;
        }
        bool iniPos = false;
        int count = 0;


        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (!(array[i, j].getType() == 0))
                {
                    roadCube.Add(array[i, j]);
                }
            }
        }
        while (iniPos == false)
        {
            count = 0;
            int a = Random.Range(1, roadCube.Count);
            copX = roadCube[a].getX();
            copZ = roadCube[a].getZ();
            Debug.Log("?");

            List<Mapunit> path = array[this.copZ, this.copX].target(array, array[Data.robber.robberZ, Data.robber.robberX], array[1, 1]);
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].getType() == 3)
                {
                    count++;
                }
            }
            if (count > 1)
            {
                iniPos = true;
            }
        }

        this.transform.position = new Vector3(copX * 5, 3, copZ * 5);
    }

    public void move(int p)
    {
        Debug.Log("cop 1");
        //this.path.Clear();
        

        if (this.pathFind == false)
        {
            if (difficult == 0)
            {
                if (Data.robber.sub == false)
                {
                    this.path = array[this.copZ, this.copX].target(array, array[Data.robber.substituteZ, Data.robber.substituteX], array[1, 1]);
                }
                else
                {
                    this.path = array[this.copZ, this.copX].target(array, array[Data.robber.robberZ, Data.robber.robberX], array[1, 1]);
                }
            }
            else
            {
                if (Data.robber.sub == false)
                {
                    if (p == 0)
                    {
                        this.path = array[this.copZ, this.copX].minimax(array, array[Data.robber.substituteZ, Data.robber.substituteX], array[Data.cop1.copZ, Data.cop1.copX]);
                    }
                    else
                    {
                        this.path = array[this.copZ, this.copX].minimax(array, array[Data.robber.substituteZ, Data.robber.substituteX], array[Data.cop.copZ, Data.cop.copX]);
                    }
                    
                }
                else
                {
                    if (p == 0)
                    {
                        this.path = array[this.copZ, this.copX].minimax(array, array[Data.robber.robberZ, Data.robber.robberX], array[Data.cop1.copZ, Data.cop1.copX]);
                    }
                    else
                    {
                        this.path = array[this.copZ, this.copX].minimax(array, array[Data.robber.robberZ, Data.robber.robberX], array[Data.cop.copZ, Data.cop.copX]);
                    }
                    
                }
            }
            
            
            Debug.Log("cop 2");
            for (int i = 0; i < path.Count; i++)
            {
                Debug.Log("cop 3");

                //Debug.Log("path: " + path[i].getX() + " , " + path[i].getZ());
                this.colorSet.Add(GameObject.Find("Cube" + array[path[i].getZ(), path[i].getX()].getNumber()).GetComponent<MeshRenderer>().material.color);
                GameObject.Find("Cube" + array[path[i].getZ(), path[i].getX()].getNumber()).GetComponent<MeshRenderer>().material.color = Color.green;
            }
            this.pathFind = true;
        }
        
        if (index < step)
        {
            Debug.Log("cop 4");
            if (this == path[0])
            {

            }else if (index < path.Count)
            {
                Quaternion direction = Quaternion.LookRotation(new Vector3(path[path.Count - index - 1].getX() * 5, 3, path[path.Count - index - 1].getZ() * 5) - this.transform.position);
                this.transform.rotation = direction;
                this.transform.Translate(0, 0, Time.fixedDeltaTime * unit);
            }
            

            timer += 1;
            if (timer == 50)
            {
                Debug.Log("cop 5");
                timer = 0;
                index++;
                if (Mathf.Abs((this.transform.position.x - Data.robber.transform.position.x))<0.001f && Mathf.Abs((this.transform.position.z - Data.robber.transform.position.z)) < 0.001f)
                {
                    GameObject root = GameObject.Find("Main Camera");
                    GameObject obj = root.transform.Find("End").gameObject;
                    obj.SetActive(true);
                    GameObject.Find("Main Camera/Information").SetActive(false);
                    GameObject.Find("Main Camera/Options/Text").GetComponent<Text>().text = "You lose!";
                    this.suc = 0;
                }
            }
        }
        else
        {
            Debug.Log("cop 7");
            if (path.Count < index)
            {
                this.copX = path[0].getX();
                this.copZ = path[0].getZ();
            }
            else
            {
                this.copX = path[path.Count - index].getX();
                this.copZ = path[path.Count - index].getZ();
            }
            
            index = 0;
            if (this.difficult == 0)
            {
                step = 4;
            }
            else
            {
                step = 5;
            }
            
            this.suc = 0;
            //Data.robber.turn = false;
            if (p == 0)
            {
                Data.cop1.suc = 1;
                Data.robber.turn = false;
            }
            else if (p == 1)
            {
                //Data.robber.turn = false;
                
                Data.gameManager.turns--;
                if(Data.robber.sub == false)
                {
                    Data.robber.sub = true;
                    GameObject.Find("Robber(Clone)").SetActive(false);
                }
                
                GameObject.Find("Main Camera/Information/Step").SetActive(true);
                GameObject.Find("Main Camera/Information/Skills").SetActive(true);
                string str = "Turns left: " + Data.gameManager.turns;
                GameObject.Find("Main Camera/Information/Turns").GetComponent<Text>().text = str;
            }
            
            this.pathFind = false;
            for (int i = 0; i < path.Count; i++)
            {
                GameObject.Find("Cube" + array[path[i].getZ(), path[i].getX()].getNumber()).GetComponent<MeshRenderer>().material.color = colorSet[i];
            }
            colorSet.Clear();
            
        }

    }

}
        

    

    

