using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemycollection;
    //public GameObject triple_shot_powerup;
    //public GameObject speed_powerup;

    

    public GameObject[] powerups;

    private bool stopspawning=false;

    PlayerMove main_player;
    Asteroid asteroid;
    
   
    // Start is called before the first frame update
    void Start()
    {
        asteroid = GameObject.Find("Asteroid").GetComponent<Asteroid>();
        main_player = GameObject.Find("Player").GetComponent<PlayerMove>();
        


    }
    bool started_spawing = true;
    private void Update()
    {
        if(asteroid.check_asteroid_destroyed())
        {
            if(started_spawing)
            {
                started_spawing = false;
                StartCoroutine(SpawnEnemy());
                StartCoroutine(Spawn_triple_shot_powerup());
            }
        }
    }
    /* code return in update and function start_spawing are for the same purpose but instead of 
      checking condition in update which is time consuming 
      use the function which is called once
     */
    public void  start_spawing()
    {
         //StartCoroutine(SpawnEnemy());
         //StartCoroutine(Spawn_triple_shot_powerup());

    }


    public void stopspawn()
    {
        stopspawning = true;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.5f);//time after which game will start
        while(stopspawning==false)
        {
            GameObject newenemy= Instantiate(enemy, new Vector3(Random.Range(-7.03f, 6.49f), 5.13f, -0.63f),Quaternion.identity);
            newenemy.transform.parent = enemycollection.transform;
            
            yield return new WaitForSeconds(2.5f);
            
        }
    }

    IEnumerator Spawn_triple_shot_powerup()
    {
        yield return new WaitForSeconds(0.5f);//time after which game will start
        while (stopspawning == false)
        {
            // make random.rnage to 0 to 3 when u add 3 poweup
         int index = Random.Range(0, 3);
         GameObject triple_shoot = Instantiate(powerups[index], new Vector3(Random.Range(-7.03f, 6.49f), 7.63f, 0), Quaternion.identity);
         yield return new WaitForSeconds(Random.Range(6,8));
           
        }

       
    }

    



}
