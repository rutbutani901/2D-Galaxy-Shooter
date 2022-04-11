using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    bool enemylaser = false;
    // Update is called once per frame
    void Update()
    {
        if (enemylaser)
        {
            movedown();
        }
        else
        {
            moveup();
        }
      
            
        
       /* if(gameObject.tag=="enemy laser")//enemy lase my
        {
            transform.Translate(Vector3.down * 8 * Time.deltaTime);

        }*/
        

    }

    public void set_enemy_laser()
    {
        enemylaser = true;
    }

    void moveup()
    {
        transform.Translate(Vector3.up * 8 * Time.deltaTime);

        if (transform.parent != null)
        {
            // Destroy(gameObject.,1f);
            Destroy(transform.parent.gameObject, 1f);

        }
        Destroy(this.gameObject, 1f);
    }
    void movedown()
    {
        transform.Translate(Vector3.down * 8 * Time.deltaTime);

        if (transform.parent != null)
        {
            // Destroy(gameObject.,1f);
            Destroy(transform.parent.gameObject, 1f);

        }
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemylaser && collision.tag == "Player")
        {
            Debug.Log("Pleyr2");
            if(!player.is_shielded)//not shield
            {
                //damage player
                player.damage();
            }
           
        }

        if(enemylaser)
        {
            if (collision.gameObject.tag == "shield")
            {
                Destroy(this.gameObject);
            }
        }
        
    }


}
