using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private GameObject player;
    private AudioSource audioS;
    public AudioClip uiSound;

    public GameObject pauseMenuUI;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioS = player.gameObject.GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audioS.PlayOneShot(uiSound); //----------------------SOM--------------------------
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        audioS.PlayOneShot(uiSound); //----------------------SOM--------------------------
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        audioS.PlayOneShot(uiSound); //----------------------SOM--------------------------
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        audioS.PlayOneShot(uiSound); //----------------------SOM--------------------------
        Application.Quit();
    }








}
