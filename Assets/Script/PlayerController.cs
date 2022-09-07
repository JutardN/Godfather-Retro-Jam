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
    public int speed;
    public float distanceMin;

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
        if (!attacking)
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
            this.transform.position = Vector3.Lerp(this.transform.position, movementPoints[pos].transform.position, Time.deltaTime * speed);
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
        }
    }

}
