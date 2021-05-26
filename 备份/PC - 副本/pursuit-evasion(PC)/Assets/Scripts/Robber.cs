using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private Transform[] positions;
    private int index = 1;
    public float speed = 10;
    public int unit = 5;
    public int suc = 1;


    public GameObject arrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        //Data.robber = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (suc == 1)
        {
            Move();
        }
        
    }
    public void Move()
    {
        
        transform.Translate(0,0,Time.deltaTime*speed
            );
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {

            GameObject.Instantiate(arrowPrefab, positions[index].position, Quaternion.LookRotation((positions[index + 1].position - positions[index].position)));
            this.suc = 0;

            
        }
    }
    public void Turn(Vector3 turnAngle)
    {
        this.transform.eulerAngles = turnAngle;
        index++;
        this.suc = 1;
    }
}
