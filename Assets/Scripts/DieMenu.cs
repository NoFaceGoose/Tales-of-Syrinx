using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject DieMenuUI;
    public GameObject Player;
    void Start()
    {
        
    }

    public void Replay()
    {
        DieMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Player.GetComponent<PlayerController>().Reborn();
    }

    public void EnterDieMenu()
    {
        DieMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
