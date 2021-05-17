using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{


    public float speed = 10;
    public float mouseSpeed = 60;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(horizontal*speed, mouse*mouseSpeed, vertical*speed) * Time.deltaTime*speed ,Space.World);
        
    }
}
