using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private PlayerController playerController;
    private SpawnController spawnController;
    public GameObject player;
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
        playerController = player.GetComponent<PlayerController>();
        spawnController = SpawnController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
