using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform[] positions;
    private int index = 0;
    public float speed = 5;
    public int unit = 5;


    public GameObject arrowPrefab;


    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        //Data.enemy = this;
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        /*
        if (index > positions.Length - 1) return;
        
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if(Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        */

        //if (direction == 1)
        //{
        //    if (index + 1 > 7)
        //    {
        //        index -= 8;
        //    }
        //    transform.Translate((positions[index+1].position - transform.position).normalized * Time.deltaTime * speed);
        //}else if (direction == 2)
        //{
        //    if (index-1 < 0)
        //    {
        //        index += 8;
        //    }
        //    transform.Translate((positions[index-1].position - transform.position).normalized * Time.deltaTime * speed);
        //}
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {

            GameObject a = GameObject.Instantiate(arrowPrefab, positions[index].position, Quaternion.LookRotation((positions[index + 1].position - positions[index].position)));
            

            index++;
        }
    }
    public void Turn(Vector3 turnAngle) {
        this.transform.eulerAngles = turnAngle;
    }

}
    
