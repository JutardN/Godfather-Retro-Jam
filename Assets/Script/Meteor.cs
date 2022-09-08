using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{
    public GameObject clone1;
    public GameObject clone2;

    private Vector3 direction;
    private Vector3 initPos;
    private Vector3 atmPos;

    private Vector3 dir1;
    private Vector3 dir2;
    private Vector3 dir3;
    private Vector3 dir4;
    private Vector3 dir5;
    private Vector3 dir6;

    [HideInInspector] public Spawner spawner;

    public SpriteRenderer moyenMeteor;
    public SpriteRenderer big;

    private float elapsedTime;

    [HideInInspector] public bool littleMeteor = false;
    [HideInInspector] public bool meteor = false;
    [HideInInspector] public bool asteroid = false;

    [HideInInspector] public float timeToDestroy;

    private int typeOfMeteorRand;
    private int directionRand;

    public int score = 0;
    private float minDuration = 2f;
    private float maxDuration = 4f;
    private float percentageCompleted;
    [Header("Probabilité entre petite et moyenne météorite")]
    public int probaLittle = 50;

    [Header("Paramètres : petite météorite")]
    public float minLittleDuration = 1f;
    public float maxLittleDuration = 3f;

    [Header("Paramètres : moyenne météorite")]
    public float minMidDuration = 1.33f;
    public float maxMidDuration = 4f;

    [Header("Paramètres : Grosse météorite")]
    public float minBigDuration = 6f;
    public float maxBigDuration = 6f;
    [HideInInspector] public bool crashing = true;

    bool end;
    private void Awake()
    {
        initPos = transform.position;

    }
    void Start()
    {
        //
        if (asteroid)
            gameObject.transform.localScale = new Vector3(8, 8, 1);
        else
            gameObject.transform.localScale = new Vector3(5, 5, 1);

        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();

        dir1 = GameObject.FindGameObjectWithTag("dir1").transform.position;
        dir2 = GameObject.FindGameObjectWithTag("dir2").transform.position;
        dir3 = GameObject.FindGameObjectWithTag("dir3").transform.position;
        dir4 = GameObject.FindGameObjectWithTag("dir4").transform.position;
        dir5 = GameObject.FindGameObjectWithTag("dir5").transform.position;
        dir6 = GameObject.FindGameObjectWithTag("dir6").transform.position;

        typeOfMeteorRand = Random.Range(1, 101);

        // Randomisation petite ou moyenne météorite
        if (spawner.asteroidEvent == true)
        {
            asteroid = true;
        }
        else
        {
            if (typeOfMeteorRand <= probaLittle)
                littleMeteor = true;
            else
                meteor = true;
        }

        if (littleMeteor)
        {
            minDuration = minLittleDuration;
            maxDuration = maxLittleDuration;
            clone1.SetActive(true);
            clone2.SetActive(true);
        }
        if (meteor)
        {
            minDuration = minMidDuration;
            maxDuration = maxMidDuration;
            gameObject.GetComponent<SpriteRenderer>().sprite = moyenMeteor.sprite;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.08f, -0.1f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.18f, 0.1f);
        }
        if (asteroid)
        {
            minDuration = minBigDuration;
            maxDuration = maxBigDuration;
            gameObject.GetComponent<SpriteRenderer>().sprite = big.sprite;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.16f, -0.13f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.32f, 0.29f);
        }

        if (initPos.x >= 0)
            directionRand = Random.Range(1, 4);
        else
        {
            directionRand = Random.Range(4, 7);
            gameObject.transform.rotation = Quaternion.Euler(0, 160, 0);

        }

        // Durée avant impact

        maxDuration = Mathf.Lerp(maxDuration, minDuration, Difficulty.GetDifficultyPercent());

        // Assignation de la direction de la météorite
        if (directionRand == 1)
            direction = dir1;
        else if (directionRand == 2)
            direction = dir2;
        else if (directionRand == 3)
            direction = dir3;
        else if (directionRand == 4)
            direction = dir4;
        else if (directionRand == 5)
            direction = dir5;
        else
            direction = dir6;

    }

    void Update()
    {
        if (!end)
        {
            atmPos = transform.position;
            elapsedTime += Time.deltaTime;
            percentageCompleted = (elapsedTime / maxDuration);

            // météor se déplaçant vers sa direction
            if (!asteroid)
                transform.position = Vector2.Lerp(initPos, direction, percentageCompleted);

            else
            {
                if (crashing)
                    transform.position = Vector2.Lerp(initPos, direction, percentageCompleted);
                else
                    transform.position = atmPos;
            }


            if (transform.position.y == direction.y)
            {
                var player = GameObject.FindObjectOfType<PlayerController>();
                ScoreSave.SaveScore(player.score);
                player.StartCoroutine(player.End());
                end = true;
            }
        }
    }
}
