using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed = 100;
    Animator anim;
    private bool asteroid_destroyed = false;
    SpawnManager spawn_manager;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spawn_manager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="laser")
        {
            Destroy(GetComponent<CircleCollider2D>());
            asteroid_destroyed = true;
            spawn_manager.start_spawing();
            anim.Play("asteroid_explode");
            audio.Play();
        }
    }
    public bool check_asteroid_destroyed()
    {
        
        return asteroid_destroyed;
    }
}
