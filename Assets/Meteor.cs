using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector2 speedMinMax;
    float speed;
    public GameObject dir1;
    public GameObject dir2;
    public GameObject dir3;
    public GameObject dir4;
    public GameObject dir5;
    public GameObject dir6;
    public Vector3 direction;

    float visibleHeightThreshold;

    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;


    }

    // Update is called once per frame
    void Update()
    {

        

        transform.Translate(Vector3.down  * speed * Time.deltaTime, Space.Self);

        if (transform.position.y < visibleHeightThreshold)
        {
            Destroy(gameObject);
        }
    }
}
