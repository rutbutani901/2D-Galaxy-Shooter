using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 7f;
    private float temporary_speed;

    public bool isplayerone = false;
    public bool isplayertwo = false;
    public GameObject laser;
    int a;
    public int lives = 3;
    Vector3 offset = new Vector3(0, 1.138f, 0);
    private float timegap = 0.2f;
    private float startvalue = 0.0f;

     public bool isplayeralive = true;
    bool istripleshotactive = false;

    public GameObject shield;
    [SerializeField]
    int score;

    public GameObject thruster;
    public GameObject player_hurt_left;
    public GameObject player_hurt_right;

    HandleUI ui;
    AudioSource audio_source;
    public AudioClip[] audio_clips;

    //0 for laser
    //1 for powerup
    
   





    Coroutine end_triple_shot_coroutine;
    [SerializeField] GameObject tripleshot;

    // Start is called before the first frame update
    void Start()
    {
         ui = GameObject.Find("UI Manager").GetComponent<HandleUI>();
        
        if(ui.coopmode==false)
        {
            transform.position = Vector3.zero;
        }
        audio_source = GetComponent<AudioSource>();
        temporary_speed = speed;

        
        

    }

    bool firsttime_tripleshot = true;
    bool firsttime_speed = true;
    
    public bool shoot = true;
    // Update is called once per frame
    void Update()
    {
       
        moveplayer();

       /* if ( CrossPlatformInputManager.GetButtonDown("Shoot") && Time.time > startvalue)
        {
            firelaser();
        }*/
        if(isplayerone)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > startvalue)
            {
                firelaser();
            }
        }
        if(isplayertwo)
        {
            if (Input.GetKey(KeyCode.Keypad2) && Time.time > startvalue)
            {
                firelaser();
            }
        }

    }

    Vector3 tripleshot_offset = new Vector3(-2.357874f, -0.049443f, -159.7894f);
   
     void firelaser()
    {
        
        startvalue = Time.time + timegap;
               
        if(istripleshotactive)
        {
            Instantiate(tripleshot, transform.position + tripleshot_offset, Quaternion.identity);
            //GameObject duplicate_triple_shot= Instantiate(tripleshot, transform.position + tripleshot_offset, Quaternion.identity);
            //Destroy(duplicate_triple_shot, 1f);
            //Destroy(tripleshot, 1f);
            
          
        }
        else
        {
            Instantiate(laser, transform.position + offset, Quaternion.identity);
        }

        audio_source.clip = audio_clips[0];
        audio_source.Play();
        
    }
    public void set_triple_shot_active()
    {
        audio_source.clip = audio_clips[1];
        audio_source.Play();

        if (firsttime_tripleshot)
        {
            istripleshotactive = true;
            firsttime_tripleshot = false;
        }
        else
        {
            istripleshotactive = true;
            StopCoroutine(end_triple_shot_coroutine);
        }


        // StopCoroutine(end_triple_shot_coroutine);
        end_triple_shot_coroutine = StartCoroutine(end_triple_shot());

    }
    public void set_triple_shot_deactive()
    {
        istripleshotactive = false;


    }


    IEnumerator end_triple_shot()
    {
        yield return new WaitForSeconds(5f);
        istripleshotactive = false;
    }

    //float up = 0f, down = 0f, right = 0f, left = 0f;
    float xpos;
    float ypos;
    void moveplayer()
    {
        if(isplayerone)
        {
            //float horizontalinput = CrossPlatformInputManager.GetAxis("Horizontal");//
            float horizontalinput = Input.GetAxis("Horizontal");
            //float verticalinput = CrossPlatformInputManager.GetAxis("Vertical");//
            float verticalinput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalinput, verticalinput, 0);

            transform.Translate(movement * speed * Time.deltaTime);
        }
        if(isplayertwo)
        {
             if(Input.GetKey(KeyCode.M))
            {
                //right = 1f;
                xpos = 1f;

            }
            else if(Input.GetKey(KeyCode.B))
            {
               // left = 1f;
                xpos= -1;

            }
            else
            {
                xpos = 0;
            }

           if (Input.GetKey(KeyCode.N))
            {
                //down = 1f;
                ypos = -1;

            }
            else if (Input.GetKey(KeyCode.H))
            {
               // up = 1f;
                ypos = 1;

            }
            else
            {
                ypos = 0;
            }

            transform.Translate(new Vector3(xpos,ypos,0)*speed*Time.deltaTime);


        }

        /*if (transform.position.x >= 8.52)
        {
            transform.position = new Vector3(8.52f, transform.position.y, 0);
        }
        if (transform.position.x <= -8.57)
        {
            transform.position = new Vector3(-8.57f, transform.position.y, 0);
        }*/
        /*if(transform.position.y>= 5.45)
        {
            transform.position = new Vector3(transform.position.x, 5.45f, 0);
        }
        if(transform.position.y <= -3.54)
        {
            transform.position = new Vector3( transform.position.x,-3.54f, 0);
        }*/
        // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.54f, 5.45f), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.57f, 8.52f), Mathf.Clamp(transform.position.y, -3.54f, 5.45f), 0);

    }

     public void damage()
    {
        lives--;
        switch (lives)
        {
            case 1:
                
                player_hurt_left.SetActive(true);
                player_hurt_right.SetActive(true);
                break;
            case 2:
                
                player_hurt_left.SetActive(true);
                break;

        }

        Debug.Log(lives);
        SpawnManager spawn = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


        if (lives <= 0)
        {
           
            ui.flicker_gameover();
            isplayeralive = false;
            // gameObject.SetActive(false);
            Destroy(this.gameObject);
            if (spawn != null)
            {
                spawn.stopspawn();


            }

           


        }

    }
    

    public int getlives()
    {
        return lives;
    }

    Coroutine speed_power_up_coroutine;

    public void speed_powerups()
    {
        audio_source.clip = audio_clips[1];
        audio_source.Play();

        if (firsttime_speed)
        {
            firsttime_speed = false;
            speed = 14f;
        }
        else
        {
            speed = 14f;
            StopCoroutine(speed_power_up_coroutine);
        }
        speed_power_up_coroutine= StartCoroutine(stop_speed_power_up());

    }

    IEnumerator stop_speed_power_up()
    {
       yield return new  WaitForSeconds(5f);
        speed = temporary_speed;
    }
    

    public bool firsttime_shield=true;
    public bool is_shielded = false;
    Coroutine shield_power_up_coroutine;
    GameObject store_shield;

   
    
    
    public void shield_powerup()
    {
        audio_source.clip = audio_clips[1];
        audio_source.Play();

        if (firsttime_shield)
        {
            is_shielded = true;
            firsttime_shield = false;
           // Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.16f, transform.position.z);
            //store_shield= Instantiate(shield, position, Quaternion.identity);

            shield.SetActive(true);
           // store_shield.transform.parent = gameObject.transform;

        }
        else
        {
            is_shielded = true;
            
            //Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.16f, transform.position.z);
             //store_shield=Instantiate(shield, position, Quaternion.identity);

            shield.SetActive(true);
            //store_shield.transform.parent = gameObject.transform;
            StopCoroutine(shield_power_up_coroutine);
        }
        shield_power_up_coroutine = StartCoroutine(stop_shield_power_up());
    }

    
    IEnumerator stop_shield_power_up()
    {
        yield return new WaitForSeconds(5f);
        //Destroy(shield);
        shield.SetActive(false);
        is_shielded = false;
    }
   

    public void updatescore()
    {
        score += 10;

    }

    public int get_score()
    {
        return score;
    }


}
