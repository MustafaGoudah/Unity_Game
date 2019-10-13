using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController: MonoBehaviour
{
    // Start is called before the first frame update
    private static SpawnController instance;
    private PlayerController playerController;
    private GameController gameController;
    private float spawnZ;
    private readonly float safeZone = 5.0f;
    private readonly float tileLength = 10.0f;
    private readonly float noOfTilesOnScreen = 15;
    private List<GameObject> instantiatedRoads;
    private List<GameObject> instantiatedObjects;
    private GameObject currentRoad;
    public GameObject roadPrefab;
    public GameObject coinPrefab;
    public GameObject blueSpherePrefab;
    public GameObject redSpherePrefab;
    public GameObject greySpherePrefab;
    public GameObject player;
    public float collectibleCycle;
    private float collectibleTimeElapsed;

    public static SpawnController Instance
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
        collectibleTimeElapsed = 0.0f;
        collectibleCycle = 1.5f;
        playerController = player.GetComponent<PlayerController>();
        gameController = GameController.Instance;
        instantiatedRoads = new List<GameObject>();
        instantiatedObjects = new List<GameObject>();

        for (int i = 0; i < noOfTilesOnScreen; i++)
        {
            SpawnRoad();
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (!(gameController.GameOver || gameController.GamePaused))
        {


            if (player.transform.position.z > (spawnZ - noOfTilesOnScreen * tileLength))
            {
                SpawnRoad();
            }

            collectibleTimeElapsed += Time.deltaTime;
            if (collectibleTimeElapsed > collectibleCycle)
            {
                SpawnCollectible();

                collectibleTimeElapsed -= collectibleCycle;
            }
            CleanMemory();

        }
    }

    private void SpawnRoad()
    {
        currentRoad = Instantiate(roadPrefab);
        currentRoad.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        instantiatedRoads.Add(currentRoad);
    }

    private void SpawnCollectible()
    {
        GameObject collectible;
        
        
            switch (Random.Range(0, 4))
            {
                case 0:
                    collectible = Instantiate(blueSpherePrefab);
                    break;
                case 1:
                    collectible = Instantiate(coinPrefab);
                    break;
                case 2:
                collectible = Instantiate(greySpherePrefab);
                
                    break;
                case 3:
                collectible = Instantiate(redSpherePrefab);
                break;
                case 4: 
                collectible = Instantiate(redSpherePrefab);
                break;
            default:
                collectible = Instantiate(redSpherePrefab);
                break;

        }
        
        collectible.transform.position = new Vector3(Random.Range(-3, 3), collectible.transform.position.y, spawnZ);
        instantiatedObjects.Add(collectible);
    }

    private void CleanMemory()
    {
        if (instantiatedObjects.Count > 0)
        {
            GameObject oldestObject = instantiatedObjects[0];
            if (player.transform.position.z > oldestObject.transform.position.z + safeZone)
            {
                Destroy(oldestObject);
                instantiatedObjects.RemoveAt(0);
            }
        }

        if (instantiatedRoads.Count > 0)
        {
            GameObject oldestRoad = instantiatedRoads[0];
            if (player.transform.position.z > oldestRoad.transform.position.z + tileLength + safeZone)
            {
                Destroy(oldestRoad);
                instantiatedRoads.RemoveAt(0);
            }
        }
    }
}
