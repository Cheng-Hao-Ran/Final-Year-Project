using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move_control : MonoBehaviour
{
    
    //private int index = 1;
    public float speed = 10;
    public int unit = 5;
    public int arrowSuc = 1;
    public int turnSuc = 0;
    public int startSuc = 1;
    public Mapunit[,] array;
    public int step = -1;
    public int timer = 0;
    public int robberX = 0;
    public int robberZ = 0;
    public int substituteX = 0;
    public int substituteZ = 0;
    public bool turn = true;
    public bool fly = true;
    public bool state = true;
    public bool sub = true;


    public GameObject arrowPrefab;

    private Text text;
    

    // Start is called before the first frame update
    void Start()
    {
        //array = RandomMap.instance.array;
        array = RandomMap.array;
        Data.robber = this;
        step = -1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    public void Move()
    {
        Debug.Log("rob 1");

        if ((this.array[robberZ, robberX].getType() != 3 && this.array[robberZ, robberX].getType() != 5 )&& arrowSuc == 1&&startSuc==1)
        {
            Debug.Log("rob 2");
            if (robberX==0 || robberX == this.array.GetLength(1)-1)
            {
                this.array[robberZ, robberX].addArrow(this.array[robberZ - 1,robberX]);
                this.array[robberZ, robberX].addArrow(this.array[robberZ + 1, robberX]);
            }
            else if (robberZ == 0 || robberZ == this.array.GetLength(0) - 1)
            {
                this.array[robberZ, robberX].addArrow(this.array[robberZ, robberX+1]);
                this.array[robberZ, robberX].addArrow(this.array[robberZ, robberX-1]);
            }
            else if(this.array[robberZ-1,robberX].getType() == 0) 
            {
                this.array[robberZ, robberX].addArrow(this.array[robberZ, robberX + 1]);
                this.array[robberZ, robberX].addArrow(this.array[robberZ, robberX - 1]);
            }
            else if (this.array[robberZ, robberX-1].getType() == 0)
            {
                this.array[robberZ, robberX].addArrow(this.array[robberZ + 1, robberX]);
                this.array[robberZ, robberX].addArrow(this.array[robberZ - 1, robberX]);
            }

                for (int i = 0; i < array[robberZ, robberX].getList().Count; i++)
            {
                Debug.Log("rob 3");

                Vector3 direction = new Vector3(array[robberZ, robberX].arrowLocation[i].getX(), 3, array[robberZ, robberX].arrowLocation[i].getZ());
                Vector3 pos = new Vector3(array[robberZ, robberX].getX(), 3, array[robberZ, robberX].getZ());
                GameObject.Instantiate(arrowPrefab, new Vector3(5 * direction.x, 3, 5 * direction.z), Quaternion.LookRotation(direction - pos));

            }
            array[robberZ, robberX].getList().Clear();
            arrowSuc = 0;
            turnSuc = 0;
            startSuc = 0;
        }


        Debug.Log("rob 21");

        if ((this.array[robberZ, robberX].getType() == 3|| this.array[robberZ, robberX].getType() == 5) &&arrowSuc==1)
        {

            for (int i = 0; i < array[robberZ, robberX].getList().Count; i++)
            {
                Debug.Log("rob 4");
                Vector3 direction = new Vector3(array[robberZ, robberX].arrowLocation[i].getX(), 3, array[robberZ, robberX].arrowLocation[i].getZ());
                Vector3 pos = new Vector3(array[robberZ, robberX].getX(), 3, array[robberZ, robberX].getZ());
                GameObject.Instantiate(arrowPrefab, new Vector3(5*direction.x,3,5*direction.z), Quaternion.LookRotation(direction - pos));
                
            }
            arrowSuc = 0;
            turnSuc = 0;
            startSuc = 0;
        }
        if (turnSuc == 1)
        {
            this.transform.Translate(0, 0, Time.fixedDeltaTime * unit);
            timer += 1;
            Debug.Log("rob 13");

            if (timer == 50)
            {
                Debug.Log("rob 4");
                if (Mathf.Abs((this.transform.position.x - Data.cop.transform.position.x)) < 0.001f && Mathf.Abs((this.transform.position.z - Data.cop.transform.position.z)) < 0.001f)
                {
                    GameObject root = GameObject.Find("Main Camera");
                    GameObject obj = root.transform.Find("End").gameObject;
                    obj.SetActive(true);
                    GameObject.Find("Main Camera/Information").SetActive(false);
                    GameObject.Find("Main Camera/Options/Text").GetComponent<Text>().text = "You lose!";
                    turnSuc = 0;

                }
                if (Mathf.Abs((this.transform.position.x - Data.cop1.transform.position.x)) < 0.001f && Mathf.Abs((this.transform.position.z - Data.cop1.transform.position.z)) < 0.001f)
                {
                    GameObject root = GameObject.Find("Main Camera");
                    GameObject obj = root.transform.Find("End").gameObject;
                    obj.SetActive(true);
                    GameObject.Find("Main Camera/Information").SetActive(false);
                    GameObject.Find("Main Camera/Options/Text").GetComponent<Text>().text = "You lose!";
                    turnSuc = 0;
                }
                step -= 1;
                timer = 0;
                arrowSuc = 1;
                Debug.Log("eulerAngles: "+this.transform.eulerAngles);
                if (this.transform.eulerAngles==new Vector3 (0,0,0))
                {
                    robberZ += 1;
                }
                else if (this.transform.eulerAngles == new Vector3(0, 180, 0))
                {
                    robberZ -= 1;
                }
                else if (this.transform.eulerAngles == new Vector3(0, 90, 0))
                {
                    robberX +=1;
                }else if (this.transform.eulerAngles == new Vector3(0, 270, 0))
                {
                    robberX -= 1;
                }
                string str = "Steps left: "+step;
                GameObject.Find("Main Camera/Information/Text").GetComponent<Text>().text = str;
                Debug.Log("rob 5");


            }
        }
        
    }

    public void teleport()
    {
        if(array[this.robberZ,this.robberX].getType()==4 || array[this.robberZ, this.robberX].getType() == 5)
        {
            if (this.state == true)
            {
                GameObject root = GameObject.Find("Main Camera");
                GameObject obj = root.transform.Find("Teleport").gameObject;
                obj.SetActive(true);
                this.fly = false;
                this.state = false;
            }
            
        }
    }

    public void Step1()
    {
        if (step == 0 || step==-1)
        {
            step = 1;
        }
        
        
    }

    public void Step2()
    {
        if (step == 0 || step == -1)
        {
            step = 2;
        }
    }
    public void Step3()
    {
        if (step == 0 || step == -1)
        {
            step = 3;
        }

    }
    public void Step4()
    {
        if (step == 0 || step == -1)
        {
            step = 4;
        }

    }
    public void Step5()
    {
        if (step == 0 || step == -1)
        {
            step = 5;
        }

    }
    public void Step6()
    {
        if (step == 0 || step == -1)
        {
            step = 6;
        }

    }
    public void Turn(Vector3 turnAngle)
    {
        this.transform.eulerAngles = turnAngle;
        //index++;
        
    }
}
