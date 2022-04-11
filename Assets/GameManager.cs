using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void single_player()
    {
        SceneManager.LoadScene(1);

    }
    public void TwoPlayer()
    {
        SceneManager.LoadScene(2);
    }
}
