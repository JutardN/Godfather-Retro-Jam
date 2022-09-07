using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] movementPoints = new GameObject[3];
    int pos;
    public string moveLeft, moveRight;
    bool attacking,inMovement;
    public BoxCollider2D colliderLeft, colliderRight;
    public float speed;
    public float distanceMin;
    public int score;

    [Header("Secondary Movement")]
    [SerializeField]
    bool secondMovement=false;
    public int speedMovement=10;
    public GameObject limitDistanceLeft, limitDistanceRight;

    void Start()
    {
        if (movementPoints.Length == 3)
        {
            this.transform.position = movementPoints[1].transform.position;
            pos = 1;
        }
        else
        {
            Debug.LogError("Not Enough Movements Points");
        }
        if(moveLeft == null || moveRight == null)
            Debug.LogError("Not Enough Movements Points");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            secondMovement = !secondMovement;

        if (!attacking)
        {
            if (!secondMovement)
            {
                if (Input.GetKeyDown(moveLeft) && !inMovement)
                {
                    if (pos > 0)
                    {
                        pos--;
                        //this.transform.position = movementPoints[pos].transform.position;
                        inMovement = true;
                    }
                }
                else if (Input.GetKeyDown(moveRight) && !inMovement)
                {
                    if (pos < 2)
                    {
                        pos++;
                        //this.transform.position = movementPoints[pos].transform.position;
                        inMovement = true;
                    }
                }
            }
            else
            {
                this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0,0) * Time.deltaTime * speedMovement;
                if (this.transform.position.x < limitDistanceLeft.transform.position.x)
                    this.transform.position = limitDistanceLeft.transform.position;
                if (this.transform.position.x > limitDistanceRight.transform.position.x)
                    this.transform.position = limitDistanceRight.transform.position;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Hit left");
                colliderRight.enabled = false;
                colliderLeft.enabled = true;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Hit right");
                colliderLeft.enabled = false;
                colliderRight.enabled = true;
            }
        }
        if (inMovement)
        {
            colliderLeft.enabled = false;
            colliderRight.enabled = false;

            this.transform.position = Vector3.Lerp(this.transform.position, movementPoints[pos].transform.position, speed);
            if (Vector3.Distance(this.transform.position, movementPoints[pos].transform.position) <= distanceMin)
            {
                this.transform.position = movementPoints[pos].transform.position;
                inMovement = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            Debug.Log("CASSER");
            score+=collision.GetComponent<Meteor>().score;
            Destroy(collision.gameObject);
        }
    }
}
