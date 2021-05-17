using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Substitute : MonoBehaviour
{
    public GameObject substitute;
    // Start is called before the first frame update
    public void Trick()
    {
        GameObject robber = GameObject.Find("Robber");
        Data.robber.substituteX = Data.robber.robberX;
        Data.robber.substituteZ = Data.robber.robberZ;
        GameObject.Instantiate(substitute, robber.transform.localPosition, Quaternion.identity);
        Data.robber.sub = false;


        this.gameObject.GetComponent<Button>().interactable = false;
    }
}
