using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private PlayerController playerController;
    private SpawnController spawnController;
    private MusicController musicController;
    public GameObject player;
    public Text timerText;
    public Text boostMeterText;
    public Text ScoreText;
    public bool GameOver;
    public bool GamePaused;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public bool inInvincibleMode;



    // Start is called before the first frame update

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        inInvincibleMode = false;
        GameOver = false;
        GamePaused = false;
        playerController = player.GetComponent<PlayerController>();
        timerText.text = playerController.timerValue.ToString("F");
        boostMeterText.text = playerController.boostMeter.ToString();
        ScoreText.text = playerController.score.ToString();
        spawnController = SpawnController.Instance;
        musicController = MusicController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        { 

            ScoreText.text = playerController.score.ToString();
            timerText.text = playerController.timerValue.ToString("F");

            boostMeterText.text = playerController.boostMeter.ToString();

            if (!playerController.alive)
            {
                gameOver();
                return;
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (GamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if(playerController.invincible && inInvincibleMode)
            {
                Invincible();
            }


        }

    }

    void gameOver()
    {
        GameOver = true;
        this.gameOverMenu.SetActive(true);
        musicController.PlayMenuMusic();
    }

    void Invincible()
    {
        inInvincibleMode = false;
        musicController.PlayInvincibleMusic();
    }
    public void Resume()
    {
        musicController.PlayGameMusic();
        this.GamePaused = false;
        this.pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        musicController.PlayMenuMusic();
        this.GamePaused = true;
        this.pauseMenu.SetActive(true);
    }
}
