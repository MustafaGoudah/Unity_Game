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
    public GameObject player;
    public Text timerText;
    public Text boostMeterText;
    public Text ScoreText;
    public bool GameOver;
    public bool GamePaused;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;


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
  
        GameOver = false;
        GamePaused = false;
        playerController = player.GetComponent<PlayerController>();
        timerText.text = playerController.timerValue.ToString("F");
        boostMeterText.text = playerController.boostMeter.ToString();
        ScoreText.text = playerController.score.ToString();
        spawnController = SpawnController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {



            ScoreText.text = playerController.score.ToString();
            timerText.text = playerController.timerValue.ToString("F");

            boostMeterText.text = playerController.boostMeter.ToString();

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
        }

        else
        {
            this.gameOverMenu.SetActive(true);
        }
    }

    public void Resume()
    {
        this.GamePaused = false;
        this.pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        this.GamePaused = true;
        this.pauseMenu.SetActive(true);
    }
}
