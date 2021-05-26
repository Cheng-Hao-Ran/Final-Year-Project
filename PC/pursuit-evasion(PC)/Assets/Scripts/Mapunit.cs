using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mapunit : MonoBehaviour
{
    private int x;
    private int z;
    private int type;
    private int number;
    private int attribute = 0;
    public int cost = 0;
    public int costH = 0;
    public int costG = 0;
    public Mapunit parent;
    public Mapunit teleport;

    public List<Mapunit> arrowLocation = new List<Mapunit>();
    public List<List<List<Mapunit>>> list = new List<List<List<Mapunit>>>();


    // Constructer
    public Mapunit(int number, int type)
    {
        this.number = number;
        this.type = type;
    }

    public Mapunit() { }


    // Get all cubes around this cube
    public List<Mapunit> current(Mapunit[,] array)
    {
        List<Mapunit> currentList = new List<Mapunit>();
        if (this.getX() == 0)
        {
            if (this.getZ() == 0)
            {
                currentList.Add(array[this.getZ(), this.getX() + 1]);
                currentList.Add(array[this.getZ() + 1, this.getX()]);
            }
            else if (this.getZ() == array.GetLength(0) - 1)
            {
                currentList.Add(array[this.getZ(), this.getX() + 1]);
                currentList.Add(array[this.getZ() - 1, this.getX()]);
            }
            else
            {
                currentList.Add(array[this.getZ(), this.getX() + 1]);
                currentList.Add(array[this.getZ() + 1, this.getX()]);
                currentList.Add(array[this.getZ() - 1, this.getX()]);
            }

        }
        else if (this.getX() == array.GetLength(1) - 1)
        {
            if (this.getZ() == 0)
            {
                currentList.Add(array[this.getZ(), this.getX() - 1]);
                currentList.Add(array[this.getZ() + 1, this.getX()]);
            }
            else if (this.getZ() == array.GetLength(0) - 1)
            {
                currentList.Add(array[this.getZ(), this.getX() - 1]);
                currentList.Add(array[this.getZ() - 1, this.getX()]);
            }
            else
            {
                currentList.Add(array[this.getZ(), this.getX() - 1]);
                currentList.Add(array[this.getZ() + 1, this.getX()]);
                currentList.Add(array[this.getZ() - 1, this.getX()]);
            }
        }
        else if (this.getZ() == 0)
        {
            currentList.Add(array[this.getZ() + 1, this.getX()]);
            currentList.Add(array[this.getZ(), this.getX() + 1]);
            currentList.Add(array[this.getZ(), this.getX() - 1]);
        }
        else if (this.getZ() == array.GetLength(0) - 1)
        {
            currentList.Add(array[this.getZ() - 1, this.getX()]);
            currentList.Add(array[this.getZ(), this.getX() + 1]);
            currentList.Add(array[this.getZ(), this.getX() - 1]);
        }
        else
        {
            currentList.Add(array[this.getZ() + 1, this.getX()]);
            currentList.Add(array[this.getZ() - 1, this.getX()]);
            currentList.Add(array[this.getZ(), this.getX() + 1]);
            currentList.Add(array[this.getZ(), this.getX() - 1]);
        }





        return currentList;
    }



    // A* algorithm
    public List<Mapunit> target(Mapunit[,] array, Mapunit end,Mapunit barrier)
    {
        List<Mapunit> openList = new List<Mapunit>();
        List<Mapunit> closeList = new List<Mapunit>();
        List<Mapunit> path = new List<Mapunit>();
        Mapunit idealPoint = new Mapunit();
        int type = barrier.getType();
        barrier.setType(0);
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j].getType() == 0)
                {
                    array[i, j].setAttribute(-1);
                }
                else
                {
                    array[i, j].setAttribute(0);
                }
            }
        }

        openList.Add(this);

        while (openList.Count > 0)
        {
            idealPoint = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].cost <= idealPoint.cost)
                {
                    idealPoint = openList[i];
                }

            }
            if (idealPoint.getX() == end.getX() && idealPoint.getZ() == end.getZ())
            {
                while (!(idealPoint.parent is null))
                {
                    path.Add(idealPoint);
                    idealPoint = idealPoint.parent;
                }
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j].clearData();
                    }
                }
                barrier.setType(type);
                return path;
            }
            else
            {
                //var remove = openList.FirstOrDefault(t => t.number == idealPoint.number);
                openList.RemoveAll(r => r.number == idealPoint.number);
                closeList.Add(idealPoint);
                idealPoint.attribute = -1;

                List<Mapunit> currentList = idealPoint.current(array);
                for (int i = 0; i < currentList.Count; i++)
                {
                    if (currentList[i].attribute == 0)
                    {
                        currentList[i].parent = idealPoint;
                        currentList[i].costH = currentList[i].Manhattan(end);
                        currentList[i].costG = idealPoint.costG + 1;
                        currentList[i].cost = currentList[i].costG + currentList[i].costH;
                        currentList[i].attribute = 1;
                        openList.Add(currentList[i]);
                    }
                }
            }
        }

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j].clearData();
            }
        }
        barrier.setType(type);
        return path;
    }


    // Get the move range of the position according to its role
    public List<Mapunit> moveRange(Mapunit[,] array, int steps,Mapunit barrier)
    {
        List<Mapunit> path=new List<Mapunit>();
        List<Mapunit> element = this.current(array);
        List<Mapunit> temp = new List<Mapunit>();
        int type = barrier.getType();
        barrier.setType(0);
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j].getType() == 0)
                {
                    array[i, j].setAttribute(-1);
                }
                else
                {
                    array[i, j].setAttribute(0);
                }
            }
        }

        element.RemoveAll(r => r.getAttribute() == -1);
        //for (int i = 0; i < element.Count; i++)
        //{
        //    Debug.Log("element list: " + element[i]);
        //}

        //for (int i = 0; i < path2.Count; i++)
        //{
        //    Debug.Log("path list: " + path2[i]);
        //}
        path = path.Union(element,new EntityComparer()).ToList();
        //List<int> a = new List<int>{ 1, 2, 3, 4 };
        //List<int> b = new List<int>{ 3, 4, 5 };
        //a = a.Union(b).ToList();
        for (int i = 1; i <steps ; i++)
        {
            temp.Clear();
            for (int j = 0; j < path.Count; j++)
            {
                element.Clear();
                element = path[j].current(array);
                element.RemoveAll(r => r.getAttribute() == -1);
                temp = temp.Union(element,new EntityComparer()).ToList();
            }
            path = path.Union(temp, new EntityComparer()).ToList();
        }
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j].clearData();
            }
        }
        barrier.setType(type);
        return path;
    }

    // Minimax
    public List<Mapunit> minimax(Mapunit[,] array, Mapunit end, Mapunit barrier,int p)
    {
        this.list.Clear();
        int copStep;
        if (p == 0)
        {
            copStep = Data.cop.step;
        }else
        {
            copStep = Data.cop1.step;
        }
        
        int robberStep = 6;
        if (this.target(array, end, array[1, 1]).Count <= copStep)
        {
            return this.target(array, end, array[1, 1]);
        }
        
        
        
        List<Mapunit> copRange = this.moveRange(array, copStep,barrier);
        List<Mapunit> anotherCopRange = barrier.moveRange(array, copStep,array[1,1]);
        List<Mapunit> robberRange = end.moveRange(array, robberStep,array[1,1]);
        
        robberRange = robberRange.Except(copRange, new EntityComparer()).ToList();
        robberRange = robberRange.Except(anotherCopRange, new EntityComparer()).ToList();
        if (robberRange.Count == 0)
        {
            List<Mapunit> path=new List<Mapunit>();
            path.Add(this);
            return path;
        }
        for (int i = 0; i < copRange.Count; i++)
        {
            this.list.Add(new List<List<Mapunit>>());
            for (int j = 0; j < robberRange.Count; j++)
            {
                this.list[i].Add(copRange[i].target(array, robberRange[j], barrier));
            }
        }

        int max = this.list[0][0].Count;
        int maxI = 0;
        int maxJ = 0;
        int min = 0;
        int minI = 0;
        int minJ = 0;
        for (int i = 0; i < this.list.Count; i++)
        {
            max = 0;
            for (int j = 0; j < this.list[i].Count; j++)
            {
                
                if (max < this.list[i][j].Count)
                {
                    max = this.list[i][j].Count;
                    maxI = i;
                    maxJ = j;
                }
            }
            if (i == 0)
            {
                min = max;
            }else if (min > max)
            {
                min = this.list[maxI][maxJ].Count;
                minI = maxI;
                minJ = maxJ;
            }
        }

        //path for next turn
        //return this.list[minI][minJ];
        
        return this.target(array,copRange[minI],array[1,1]);
        
    }

    // Manhattan distance
    public int Manhattan(Mapunit end)
    {
        return (Mathf.Abs(this.getX() - end.getX()) + Mathf.Abs(this.getZ() - end.getZ()));
    }

    // Reset data
    public void clearData()
    {
        this.attribute = 0;
        this.cost = 0;
        this.costG = 0;
        this.costH = 0;
        this.parent = null;
        
    }

    // Package
    public int getX()
    {
        return this.x;
    }
    public int getZ()
    {
        return this.z;
    }
    public int getNumber()
    {
        return this.number;
    }

    public int getType()
    {
        return this.type;
    }

    public Mapunit getTeleport()
    {
        return this.teleport;
    }

    public List<Mapunit> getList()
    {
        return this.arrowLocation;
    }

    public int getAttribute()
    {
        return this.attribute;
    }

    public void setX(int x)
    {
        this.x=x;
    }
    public void setZ(int z)
    {
        this.z = z;
    }

    public void setNumber(int number)
    {
        this.number=number;
    }

    public void setType(int type)
    {
        this.type = type;
    }

    public void setTeleort(Mapunit teleport)
    {
        this.teleport = teleport;
    }
    public void setAttribute(int attribute)
    {
        this.attribute = attribute;
    }

    public void addArrow(Mapunit unit) 
    {
        //if (!this.arrowLocation.Contains(unit))
        //{
            this.arrowLocation.Add(unit);
        //}
        
    }



}
