using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector2 speedMinMax;
    public Vector3 direction;
    private Vector3 initPos;

    public Vector3 dir1;
    public Vector3 dir2;
    public Vector3 dir3;
    public Vector3 dir4;
    public Vector3 dir5;
    public Vector3 dir6;

    private float elapsedTime;
    public float duration = 4f;

    private void Awake()
    {
        initPos = transform.position;
    }
    void Start()
    {       
        dir1 = GameObject.FindGameObjectWithTag("dir1").transform.position;
        dir2 = GameObject.FindGameObjectWithTag("dir2").transform.position;
        dir3 = GameObject.FindGameObjectWithTag("dir3").transform.position;
        dir4 = GameObject.FindGameObjectWithTag("dir4").transform.position;
        dir5 = GameObject.FindGameObjectWithTag("dir5").transform.position;
        dir6 = GameObject.FindGameObjectWithTag("dir6").transform.position;

        int random = Random.Range(1, 6);

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
        float percentageCompleted = (elapsedTime / duration);

        transform.position = Vector2.Lerp(initPos, direction, percentageCompleted);

        if (transform.position.y == dir1.y || transform.position.y == dir2.y || transform.position.y == dir3.y || transform.position.y == dir4.y || transform.position.y == dir5.y || transform.position.y == dir6.y)
        {
            Destroy(gameObject);
        }
    }
}
