using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleUI : MonoBehaviour
{
    public bool coopmode = false;
    Text score_text;
    Image lives;
    PlayerMove player;
    public Sprite[] lives_image;

    bool game_ended = false;


    public GameObject gameover;//gameover object
    public GameObject restart_game;
    public GameObject pause_menu_panel;

    private Animator anim;


    void Start()
    {
        lives= GameObject.Find("Lives_image").GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        score_text = GameObject.Find("Score").GetComponent<Text>();
        //pause_menu_panel = GameObject.Find("Pause_Menu_Panel");
        
    }


   
    void Update()
    {
        change_score();
        change_lives_image();
        //flicker_gameover();

        if(game_ended)
        {
            if (Input.GetKey(KeyCode.R))
            {
                Invoke("load_scene", 2f);
            }
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.P))
        {
            
            pause_menu_panel.SetActive(true);
            anim = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;

            anim.SetBool("canpause", true);
            Time.timeScale = 0;

        }

    }
    void load_scene()
    {
        SceneManager.LoadScene(1);
       
    }
    void change_score()
    {
        score_text.text = "Score: " + player.get_score().ToString();
    }

    void change_lives_image()
    {
        int player_lives=player.getlives();
        
        switch(player_lives)
        {
            case 3:
                lives.sprite = lives_image[3];
                break;

            case 2:
                lives.sprite = lives_image[2];
                break;
            case 1:
                lives.sprite = lives_image[1];
                break;
            case 0:

                lives.sprite = lives_image[0];
                break;
            default:
                lives.sprite = lives_image[0];
                break;

        }
    }

    
    public void flicker_gameover()
    {
        
           
            if (player.getlives() == 0)
            {
                game_ended = true;
                restart_game.SetActive(true);
                StartCoroutine(set_flicker());
            }
        
    }

    IEnumerator set_flicker()
    {
        
        gameover.SetActive(true);
       
        yield return new WaitForSeconds(0.8f);
        gameover.SetActive(false);
       
        yield return new WaitForSeconds(0.8f);

        StartCoroutine(set_flicker());
    }

    public void resume_game()
    {
        pause_menu_panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void back_to_main_menu()
    {
        SceneManager.LoadScene(0);
    }
    
}
