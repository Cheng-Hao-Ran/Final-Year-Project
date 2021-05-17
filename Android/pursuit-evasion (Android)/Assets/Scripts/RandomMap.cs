using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public GameObject Cube;
    public static RandomMap instance;
    public static Mapunit[,] array;

    // Start is called before the first frame update
    void Start()
    {
        array = Ranmap();
        Build(array);
    }

    private void Awake()
    {
        instance = this;
    }
    public RandomMap()
    {
        
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
    Mapunit[,] Ranmap()
    {
        //Mapunit a = new Mapunit(1, 2);
        //a.setNumber(1);

        // Set rectangle road, 0 for barrier, 1 for road, 2 for road cannot be corner, 3 for corner
        Mapunit[,] array = new Mapunit[12, 12];

        //test and set number
        int num = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Mapunit unit = new Mapunit();
                unit.setNumber(num);
                unit.setX(j);
                unit.setZ(i);
                array[i, j] = unit;
                
                num++;
                
            }
        }


        // Set rectangle road, 1 for road, 0 for barrier
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {

                if (i == 0 || i == array.GetLength(0) - 1)
                {
                    array[i, j].setType(1);
                }
                else if (j == 0 || j == array.GetLength(1) - 1)
                {
                    array[i, j].setType(1);
                }
            }
        }

        //Set four corners,connect and arrow location of them. 3 for corner, 2 for road cannot be corner

        //Set corner for (0, 0)
        array[0, 0].setType(3);
        array[0, 0].addArrow(array[0, 1]);
        array[0, 0].addArrow(array[1, 0]);
        //array[0, 1].setType(2);
        //array[1, 0].setType(2);

        // Set corner for (0,array.GetLength(1)-1)
        array[0, array.GetLength(1) - 1].setType(3);
        array[0, array.GetLength(1) - 1].addArrow(array[1, array.GetLength(1) - 1]);
        array[0, array.GetLength(1) - 1].addArrow(array[0, array.GetLength(1) - 2]);
        //array[1, array.GetLength(1) - 1].setType(2);
        //array[0, array.GetLength(1) - 2].setType(2);


        // Set corner for (array.GetLength(0)-1,0)
        array[array.GetLength(0) - 1, 0].setType(3);
        array[array.GetLength(0) - 1, 0].addArrow(array[array.GetLength(0) - 2, 0]);
        array[array.GetLength(0) - 1, 0].addArrow(array[array.GetLength(0) - 1, 1]);
        //array[array.GetLength(0) - 2, 0].setType(2);
        //array[array.GetLength(0) - 1, 1].setType(2);

        // Set corner for (array.GetLength(0)-1,array.GetLength(1) - 1)
        array[array.GetLength(0) - 1, array.GetLength(1) - 1].setType(3);
        array[array.GetLength(0) - 1, array.GetLength(1) - 1].addArrow(array[array.GetLength(0) - 2, array.GetLength(1) - 1]);
        array[array.GetLength(0) - 1, array.GetLength(1) - 1].addArrow(array[array.GetLength(0) - 1, array.GetLength(1) - 2]);
        //array[array.GetLength(0) - 2, array.GetLength(1) - 1].setType(2);
        //array[array.GetLength(0) - 1, array.GetLength(1) - 2].setType(2);





        // Random road

        // 1. A line from left to right
        int a = Random.Range(2, array.GetLength(0)-3);
        for (int j = 0; j < array.GetLength(1); j++)
        {
            array[a, j].setType(1);
        }
        array[a, 0].setType(3);
        array[a, 0].addArrow(array[a + 1, 0]);
        array[a, 0].addArrow(array[a - 1, 0]);
        array[a, 0].addArrow(array[a, 1]);

        //array[a + 1, 0].setType(2);
        //array[a - 1, 0].setType(2);
        array[a, array.GetLength(1) - 1].setType(3);
        array[a, array.GetLength(1) - 1].addArrow(array[a + 1, array.GetLength(1) - 1]);
        array[a, array.GetLength(1) - 1].addArrow(array[a - 1, array.GetLength(1) - 1]);
        array[a, array.GetLength(1) - 1].addArrow(array[a , array.GetLength(1) - 2]);
        //array[a+1, array.GetLength(1) - 1].setType(2);
        //array[a-1, array.GetLength(1) - 1].setType(2);




        // 2. Two lines from up to the first line
        int b1 = Random.Range(2, array.GetLength(1)/2);
        for (int i = a; i < array.GetLength(0); i++)
        {
            array[i, b1].setType(1);
        }
        array[a, b1].setType(3);
        array[a, b1].addArrow(array[a, b1 - 1]);
        array[a, b1].addArrow(array[a, b1 + 1]);
        array[a, b1].addArrow(array[a+1, b1]);

        //array[a, b1-1].setType(2);
        //array[a, b1+1].setType(2);
        array[array.GetLength(0)-1, b1].setType(3);
        array[array.GetLength(0) - 1, b1].addArrow(array[array.GetLength(0) - 1, b1 - 1]);
        array[array.GetLength(0) - 1, b1].addArrow(array[array.GetLength(0) - 1, b1 + 1]);
        array[array.GetLength(0) - 1, b1].addArrow(array[array.GetLength(0) - 2, b1]);

        //array[array.GetLength(0)-1, b1+1].setType(2);
        //array[array.GetLength(0)-1, b1-1].setType(2);



        int b2 = Random.Range(b1 + 3, array.GetLength(1) - 3);
        for (int i = a; i < array.GetLength(0); i++)
        {
            array[i, b2].setType(1);
        }
        array[a, b2].setType(3);
        array[a, b2].addArrow(array[a, b2 - 1]);
        array[a, b2].addArrow(array[a, b2 + 1]);
        array[a, b2].addArrow(array[a + 1, b2]);
        //array[a, b2 - 1].setType(2);
        //array[a, b2 + 1].setType(2);
        array[array.GetLength(0)-1, b2].setType(3);
        array[array.GetLength(0) - 1, b2].addArrow(array[array.GetLength(0) - 1, b2 - 1]);
        array[array.GetLength(0) - 1, b2].addArrow(array[array.GetLength(0) - 1, b2 + 1]);
        array[array.GetLength(0) - 1, b2].addArrow(array[array.GetLength(0) - 2, b2]);

        //array[array.GetLength(0)-1, b2 + 1].setType(2);
        //array[array.GetLength(0)-1, b2 - 1].setType(2);




        // 3. Two lines from bottom to the first line
        int c1 = Random.Range(2, array.GetLength(1) / 2);
        for (int i = 0; i < a+1; i++)
        {
            array[i, c1].setType(1);
        }
        array[0, c1].setType(3);
        array[0, c1].addArrow(array[0, c1+1]);
        array[0, c1].addArrow(array[0, c1 - 1]);
        array[0, c1].addArrow(array[1, c1]);
        //array[0, c1 - 1].setType(2);
        //array[0, c1 + 1].setType(2);
        array[a, c1].setType(3);
        array[a, c1].addArrow(array[a, c1 + 1]);
        array[a, c1].addArrow(array[a, c1 - 1]);
        array[a, c1].addArrow(array[a-1, c1]);
        //array[a, c1 + 1].setType(2);
        //array[a, c1 - 1].setType(2);



        int c2 = Random.Range(c1 + 3, array.GetLength(1) - 3);
        for (int i = 0; i < a + 1; i++)
        {
            array[i, c2].setType(1);
        }
        array[0, c2].setType(3);
        array[0, c2].addArrow(array[0, c2 + 1]);
        array[0, c2].addArrow(array[0, c2 - 1]);
        array[0, c2].addArrow(array[1, c2]);
        //array[0, c2 - 1].setType(2);
        //array[0, c2+ 1].setType(2);
        array[a, c2].setType(3);
        array[a, c2].addArrow(array[a, c2 + 1]);
        array[a, c2].addArrow(array[a, c2 - 1]);
        array[a, c2].addArrow(array[a-1, c2]);
        //array[a, c2 + 1].setType(2);
        //array[a, c2 - 1].setType(2);

        if (a <= 5)
        {
            int a2 = 7;
            int a3 = a2 + Random.Range(0, 3);
            
            for(int i = 1; i < b1; i++)
            {
                array[a2,i].setType(1);
            }
            array[a2, 0].setType(3);
            array[a2, 0].addArrow(array[a2 - 1, 0]);
            array[a2, 0].addArrow(array[a2 + 1, 0]);
            array[a2, 0].addArrow(array[a2 , 1]);

            array[a2, b1].setType(3);
            array[a2, b1].addArrow(array[a2 - 1, b1]);
            array[a2, b1].addArrow(array[a2 + 1, b1]);
            array[a2, b1].addArrow(array[a2 , b1-1]);


            for (int i = b1; i < b2; i++)
            {
                array[a3, i].setType(1);
            }
            array[a3, b1].setType(3);
            array[a3, b1].addArrow(array[a3 - 1, b1]);
            array[a3, b1].addArrow(array[a3 + 1, b1]);
            array[a3, b1].addArrow(array[a3, 1+b1]);

            array[a3, b2].setType(3);
            array[a3, b2].addArrow(array[a3 - 1, b2]);
            array[a3, b2].addArrow(array[a3 + 1, b2]);
            array[a3, b2].addArrow(array[a3, b2 - 1]);


        }
        else
        {
            int a2 = 4;
            int a3 = a2 - Random.Range(0, 3);

            for (int i = 1; i < c1; i++)
            {
                array[a2, i].setType(1);
            }
            array[a2, 0].setType(3);
            array[a2, 0].addArrow(array[a2 - 1, 0]);
            array[a2, 0].addArrow(array[a2 + 1, 0]);
            array[a2, 0].addArrow(array[a2, 1]);

            array[a2, c1].setType(3);
            array[a2, c1].addArrow(array[a2 - 1, c1]);
            array[a2, c1].addArrow(array[a2 + 1, c1]);
            array[a2, c1].addArrow(array[a2, c1 - 1]);


            for (int i = c1; i < c2; i++)
            {
                array[a3, i].setType(1);
            }
            array[a3, c1].setType(3);
            array[a3, c1].addArrow(array[a3 - 1, c1]);
            array[a3, c1].addArrow(array[a3 + 1, c1]);
            array[a3, c1].addArrow(array[a3, 1 + c1]);

            array[a3, c2].setType(3);
            array[a3, c2].addArrow(array[a3 - 1, c2]);
            array[a3, c2].addArrow(array[a3 + 1, c2]);
            array[a3, c2].addArrow(array[a3, c2 - 1]);
        }




        List<Mapunit> roadCube = new List<Mapunit>();
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

        int gate1 = Random.Range(1, roadCube.Count);
        int gate2 = Random.Range(1, roadCube.Count);
        List<Mapunit> path=new List<Mapunit>();
        while (path.Count < 10)
        {
            gate2 = Random.Range(1, roadCube.Count);
            while (gate1 == gate2)
            {
                gate2 = Random.Range(1, roadCube.Count);
            }
            path = array[roadCube[gate1].getZ(), roadCube[gate1].getX()].target(array, (array[roadCube[gate2].getZ(), roadCube[gate2].getX()]),array[1,1]);
        }
        
        
        
        


        if (array[roadCube[gate1].getZ(), roadCube[gate1].getX()].getType() != 3)
        {
            array[roadCube[gate1].getZ(), roadCube[gate1].getX()].setType(4);
        }
        else
        {
            array[roadCube[gate1].getZ(), roadCube[gate1].getX()].setType(5);
        }

        if (array[roadCube[gate2].getZ(), roadCube[gate2].getX()].getType() != 3)
        {
            array[roadCube[gate2].getZ(), roadCube[gate2].getX()].setType(4);
        }
        else
        {
            array[roadCube[gate2].getZ(), roadCube[gate2].getX()].setType(5);
        }
        array[roadCube[gate1].getZ(), roadCube[gate1].getX()].setTeleort(array[roadCube[gate2].getZ(), roadCube[gate2].getX()]);
        array[roadCube[gate2].getZ(), roadCube[gate2].getX()].setTeleort(array[roadCube[gate1].getZ(), roadCube[gate1].getX()]);
        return array;
    }


    public List<Mapunit> roadCube()
    {
        List<Mapunit> roadCube = new List<Mapunit>();
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
        return roadCube;
    }
    void Build(Mapunit[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Vector3 pos = new Vector3(j * 5, 0, i * 5);

                GameObject obj = GameObject.Instantiate(Cube, pos, Quaternion.identity);

                obj.name = "Cube" + array[i, j].getNumber();
                
                if (array[i, j].getType() == 0)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.white;
                }

                if (array[i, j].getType() == 1)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.blue;
                }

                if (array[i, j].getType() == 4)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.red;
                }

                if (array[i, j].getType() == 3)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }

                if (array[i, j].getType() == 5)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    
    


    
    


}
