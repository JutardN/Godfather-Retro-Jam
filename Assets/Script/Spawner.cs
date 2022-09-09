using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meteor;
    public GameObject spawner;
    private GameObject newBlock;

    public bool asteroidEvent = false;

    public bool inProgress;

    float nextSpawnTime;
    public int numberOfSpawns;

    [Header("Largeur du spawner = largeur de l'écran x le multiplier")]
    public float multiplier = 1;
    [HideInInspector] public float spawnAngleMax;

    [HideInInspector] public Vector2 spawnSizeMinMax;
    [Header("CD de spawn minimal et maximal")]
    public Vector2 secondsBetweenSpawnsMinMax;

    private Vector2 spawnPosition;



    [Header("Plage de temps en seconde où le gros météor apparait")]
    public Vector2 asteroidTimingMinMax;
    [HideInInspector] public float asteroidTiming = 5f;

    Vector2 screenHalfSizeWorldUnits;

    void Start()
    {
        // Largeur du spawner ( largeur d'écran x multiplicateur ) 
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * multiplier, Camera.main.orthographicSize);
        spawner.transform.localScale = new Vector3(1, spawner.transform.localScale.y * multiplier, 1);

        asteroidTiming = Random.Range(asteroidTimingMinMax.x, asteroidTimingMinMax.y);
    }

    void Update()
    {
        //Debug.Log(Time.time);
        if (Time.timeSinceLevelLoad >= asteroidTiming)
            asteroidEvent = true;
        else
            asteroidEvent = false;

        if (!asteroidEvent)
            NormalSpawning();
        else
        {
            if (inProgress == false)
            {
                float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
                Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
                GameObject newBlock = (GameObject)Instantiate(meteor, spawnPosition, Quaternion.Euler(Vector3.forward));
                numberOfSpawns++;
                inProgress = true;
            }

        }
    }

    public void NormalSpawning()
    {
        // Assignation des paramètres du météor lors du spawn
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x);


            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
            newBlock = (GameObject)Instantiate(meteor, spawnPosition, Quaternion.Euler(Vector3.forward));

            numberOfSpawns++;
            newBlock.transform.localScale = Vector2.one * spawnSize;


            //Debug.Log(Difficulty.GetDifficultyPercent());
        }
    }

}
