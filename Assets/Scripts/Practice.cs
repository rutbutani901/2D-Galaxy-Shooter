using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float minvalue = 0.0f;
    float gap = 0.1f;
    // Update is called once per frame
    void Update()
    {
        move();

        if(Input.GetKey(KeyCode.Space) & Time.time>minvalue)
        {
            minvalue = Time.time + gap;
            firelaser();
        }
    }
    void move()
    {

        float hinput = Input.GetAxis("Horizontal");
        float vinput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(hinput, vinput, 0) * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5),
            Mathf.Clamp(transform.position.x, -5, 5),
            0);
    }
    GameObject bullet;
    Transform position,rotation;
    
    void firelaser()
    {
       GameObject temp_bullet =Instantiate(bullet,position,rotation);
       

    }
}
