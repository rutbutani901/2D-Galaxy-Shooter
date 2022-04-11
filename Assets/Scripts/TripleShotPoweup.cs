using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPoweup : MonoBehaviour
{
    PlayerMove activate_powerups;

   
   

    [SerializeField] int powerups_ID;
    // Start is called before the first frame update
    void Start()
    {
        activate_powerups = GameObject.Find("Player").GetComponent<PlayerMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }

    
    
    void move()
    {
        transform.Translate(Vector3.down * 3 * Time.deltaTime);

        if(transform.position.y<=-5.87)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {

            Debug.Log(collision.transform.name);
          
            switch (powerups_ID)
            {
                case 0://tripleshot
                    activate_powerups.set_triple_shot_active();
                    break;
                case 1://speed
                    activate_powerups.speed_powerups();
                    break;
                case 2:
                    activate_powerups.shield_powerup();
                    break;
            }
            Destroy(gameObject);
        }
    }

}
