using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EntityComparer : IEqualityComparer<Mapunit>
{
    public bool Equals(Mapunit a, Mapunit b)
    {
        if (Object.ReferenceEquals(a, b)) return true;
        if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
            return false;

        return a.getNumber() == b.getNumber()&& a.getX()== b.getX()&& a.getZ()== b.getZ()&& a.getType()==b.getType()&& a.getAttribute()==b.getAttribute()&& a.cost==b.cost&&a.costH==b.costH&&a.costG==b.costG&&a.parent==b.parent&&a.teleport==b.teleport&&a.arrowLocation==b.arrowLocation&&a.list==b.list;
    }

    public int GetHashCode(Mapunit a)
    {
        if (Object.ReferenceEquals(a, null)) return 0;
        


        int hashX = a.getX().GetHashCode();
        int hashZ = a.getZ().GetHashCode();
        int hashType = a.getType().GetHashCode();
        int hashNumber = a.getNumber().GetHashCode();
        int hashAttribute = a.getAttribute().GetHashCode();
        int hashCost = a.cost.GetHashCode();
        int hashCostH = a.costH.GetHashCode();
        int hashCostG = a.costG.GetHashCode();
        int hashParent = a.parent == null ? 0 : a.parent.GetHashCode();
        int hashTeleport = a.teleport == null ? 0 : a.teleport.GetHashCode();
        int hashArrowLocation = a.arrowLocation == null ? 0 : a.arrowLocation.GetHashCode();
        int hashList = a.list == null ? 0 : a.list.GetHashCode();

        return hashX ^ hashZ ^ hashType ^ hashNumber ^ hashAttribute ^ hashCost ^ hashCostH ^ hashCostG ^ hashParent ^ hashTeleport ^ hashArrowLocation ^ hashList;
    }

}
//private int x;
//private int z;
//private int type;
//private int number;
//private int attribute = 0;
//public int cost = 0;
//public int costH = 0;
//public int costG = 0;
//public Mapunit parent;
//public Mapunit teleport;

//public List<Mapunit> arrowLocation = new List<Mapunit>();
//public List<List<List<Mapunit>>> list = new List<List<List<Mapunit>>>();