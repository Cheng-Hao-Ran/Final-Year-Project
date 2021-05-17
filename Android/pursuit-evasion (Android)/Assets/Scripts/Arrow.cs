using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnMouseUpAsButton()
    {
        if (this != null)
        {
            Debug.Log("oulajiao: " + this.transform.eulerAngles);
            Data.robber.turnSuc = 1;
            GameObject[] a = GameObject.FindGameObjectsWithTag("arrow");

            Data.robber.Turn(this.transform.eulerAngles);

            for (int i = 0; i < a.Length; i++)
            {
                GameObject.DestroyImmediate(a[i]);
            }
        }
        

    }

}
