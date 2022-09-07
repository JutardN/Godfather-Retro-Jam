using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meteor;

    float nextSpawnTime;
    public float widthMax = 1;
    public float spawnAngleMax;

    public Vector2 spawnSizeMinMax;
    public Vector2 secondsBetweenSpawnsMinMax;

    Vector2 screenHalfSizeWorldUnits;

    void Start()
    {
        // Largeur du spawner ( largeur d'écran x multiplicateur ) 
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * widthMax, Camera.main.orthographicSize);
    }

    void Update()
    {
        // Assignation des paramètres du météor lors du spawn
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x);

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
            GameObject newBlock = (GameObject)Instantiate(meteor, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newBlock.transform.localScale = Vector2.one * spawnSize;

            Debug.Log(Difficulty.GetDifficultyPercent());
        }

    }
}
