using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 direction;
    private Vector3 initPos;

    private Vector3 dir1;
    private Vector3 dir2;
    private Vector3 dir3;
    private Vector3 dir4;
    private Vector3 dir5;
    private Vector3 dir6;

    public bool asteroid;

    private float elapsedTime;
    public float duration = 4f;
    public float minDuration = 2f;
    private float percentageCompleted;
    public float timeToDestroy;

    private int random;

    public int score=0;
    private void Awake()
    {
        initPos = transform.position;
    }
    void Start()
    {
        //
        if(asteroid)
            gameObject.transform.localScale = new Vector3(8, 8, 1);
        else
        gameObject.transform.localScale = new Vector3(3, 3, 1); // A MODIFIER PLUS TARD ET INSTANTIER UN AUTRE PREFAB DANS LE SPAWNER

        dir1 = GameObject.FindGameObjectWithTag("dir1").transform.position;
        dir2 = GameObject.FindGameObjectWithTag("dir2").transform.position;
        dir3 = GameObject.FindGameObjectWithTag("dir3").transform.position;
        dir4 = GameObject.FindGameObjectWithTag("dir4").transform.position;
        dir5 = GameObject.FindGameObjectWithTag("dir5").transform.position;
        dir6 = GameObject.FindGameObjectWithTag("dir6").transform.position;

        if (initPos.x >= 0)
            random = Random.Range(1, 4);
        else
        {
            random = Random.Range(4, 7);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-gameObject.GetComponent<BoxCollider2D>().offset.x, gameObject.GetComponent<BoxCollider2D>().offset.y);
        }

        // Durée avant impact

        duration = Mathf.Lerp(duration, minDuration, Difficulty.GetDifficultyPercent());

        // Assignation de la direction de la météorite
        if (random == 1)
            direction = dir1;
        else if (random == 2)
            direction = dir2;
        else if (random == 3)
            direction = dir3;
        else if (random == 4)
            direction = dir4;
        else if (random == 5)
            direction = dir5;
        else
            direction = dir6;

    }

    void Update()
    {

        elapsedTime += Time.deltaTime;
        percentageCompleted = (elapsedTime / duration);

        // météor se déplaçant vers sa direction
        if (!asteroid)
            transform.position = Vector2.Lerp(initPos, direction, percentageCompleted);

        else
            StartCoroutine(Asteroid());

        if (transform.position.y == direction.y)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Asteroid()
    {
        transform.position = Vector2.Lerp(initPos, direction * 0.3f, percentageCompleted );
        yield return new WaitForSeconds(timeToDestroy);
        Debug.Log("Explosion");
        StopAllCoroutines();
    }
}
