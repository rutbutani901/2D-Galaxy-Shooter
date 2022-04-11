using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator anim;
    Vector3 initialposition;
    PlayerMove player;
    public float speed;
    AudioSource audio;
    public GameObject enemy_laser;

   /* public GameObject laser;
    Vector3 left_laser_offset;//difference between enemy and left laser
    Vector3 right_laser_offset;//difference between enemy and right laser
   */
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        initialposition = transform.position;
        /*StartCoroutine(firelaser());// enemy laser my
         left_laser_offset = new Vector3(0.184f, 1.448f, 0);
         right_laser_offset = new Vector3(-0.18f, 1.448f, 0);*/
    }

    float minvalue = 0.12f;
    float gap = 4f;

    Vector3 enemy_laser_offet = new Vector3(0.523805f, 1.3243363f,0);
    // Update is called once per frame
    void Update()
    {
        movement();

        if(Time.time>minvalue)
        {
            minvalue = Time.time + gap;
           GameObject enemylaser =Instantiate(enemy_laser, transform.position-enemy_laser_offet, Quaternion.identity);
            // Laser[] lasers_of_enemy = enemylaser.GetComponentInChildren<Laser>(); 
           Laser[] lasers_of_enemy= enemylaser.GetComponentsInChildren<Laser>();

            for(int i=0; i<lasers_of_enemy.Length;i++)
            {
                lasers_of_enemy[i].set_enemy_laser();
            }
        }
    }
    void movement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -5.32)
        {
            Destroy(this.gameObject);
            //transform.position = new Vector3(Random.Range(-7.03f, 6.49f), initialposition.y, initialposition.z);
        }
    }


    //bool enemy_destroyed=false;//in revision focus on this variable 

    /*IEnumerator firelaser()
    {
        yield return new WaitForSeconds(0.1f);
        
            GameObject left_laser= Instantiate(laser, transform.position - left_laser_offset, Quaternion.identity);
            left_laser.transform.localScale = new Vector3(0.5f, 0.24f, 0);
            GameObject right_laser = Instantiate(laser, transform.position - right_laser_offset, Quaternion.identity);
            right_laser.transform.localScale = new Vector3(0.5f, 0.24f, 0);
            yield return new WaitForSeconds(3f);
        StartCoroutine(firelaser());


    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="Player")
        {
            Debug.Log("player");
            //damage the player
            // other.GetComponent<PlayerMove>().damage();
            //other.transform.GetComponent<PlayerMove>().damage();

            // player= other.GetComponent<PlayerMove>();
            
            if(!player.is_shielded)
            {
                if (player != null)
                {
                    
                        player.damage();
                    
                }
            }



            speed = 0;
            // anim.SetTrigger("enemy_explode");
            anim.SetBool("enemy_explode", true);
            audio.Play();
            player.updatescore();
            Destroy(GetComponent<BoxCollider2D>());//destroys collider so that player can't hit it back
            Destroy(this.gameObject,1f);//enemy dies
            
        }
        if(other.tag=="laser")
        {
          //if(!enemy_destroyed)
            // {
              //  enemy_destroyed = true;
                speed = 0;
                //anim.SetTrigger("enemy_explode");
                //anim.SetBool("enemy_explode", true);
                anim.SetTrigger("enemy_explode");
                audio.Play();
                // anim.Play("enemy_explosion");
                player.updatescore();
                Destroy(other.gameObject);
            Destroy(GetComponent<BoxCollider2D>());//destroys collider so that laser can't hit it back
            //StartCoroutine(delay());
            // other.gameObject.SetActive(false); 
            Destroy(this.gameObject, 1f);
                

            //}
        }
        if(other.tag=="shield")
        {

            player.updatescore();
            anim.SetTrigger("enemy_explode");
            audio.Play();
            Destroy(GetComponent<BoxCollider2D>());//destroys collider so that shield can't hit it back
            Destroy(this.gameObject,1f);
        }

        //enemy explode
        

    }
    /*IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        enemy_destroyed = false;
    }*/
}
