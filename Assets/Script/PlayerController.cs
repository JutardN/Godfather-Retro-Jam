using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject[] movementPoints = new GameObject[3];
    int pos;
    public string moveLeft, moveRight;
    public string hitLeft, hitRight;
    public string saveHitTouch;
    bool attacking, inMovement, spamming;
    public BoxCollider2D collider;
    public float speed;
    public float distanceMin;
    public int score;
    float currentTimer = 0;
    float currentTimerSpam;
    public float timerMeteor;
    public float timerMeteorSpam;
    SpriteRenderer sprite;
    Animator anim;
    Spawner spawner;

    Meteor currentMeteor;

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
        if (moveLeft == "" || moveRight == "")
            Debug.LogError("No Key to move");
        if (hitLeft == "" || hitRight == "")
            Debug.LogError("No Key to attack");

        currentTimerSpam = timerMeteorSpam;
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!attacking && !inMovement && !spamming)
        {
            if (Input.GetKeyDown(moveLeft) && !inMovement)
            {
                if (pos > 0)
                {
                    pos--;
                    inMovement = true;
                    sprite.flipX = false;
                }
            }
            else if (Input.GetKeyDown(moveRight) && !inMovement)
            {
                if (pos < 2)
                {
                    pos++;
                    inMovement = true;
                    sprite.flipX = true;
                }
            }

            if (Input.GetKeyDown(hitLeft))
            {
                collider.offset = new Vector2(-1, 0.5f);
                sprite.flipX = false;
                anim.SetTrigger("Attacking");
                attacking = true;
                saveHitTouch = hitLeft;
            }
            else if (Input.GetKeyDown(hitRight))
            {
                collider.offset = new Vector2(1, 0.5f);
                sprite.flipX = true;
                anim.SetTrigger("Attacking");
                attacking = true;
                saveHitTouch = hitRight;
            }

        }
        if (inMovement)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, movementPoints[pos].transform.position, speed);
            if (Vector3.Distance(this.transform.position, movementPoints[pos].transform.position) <= distanceMin)
            {
                this.transform.position = movementPoints[pos].transform.position;
                inMovement = false;
            }
        }
        if (attacking && collider.enabled == true && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            Debug.Log("fini");
            attacking = false;
            collider.enabled = false;
        }

        if (spamming)
        {
            
            currentTimer += Time.deltaTime;
            currentTimerSpam -= Time.deltaTime;

            if (currentTimerSpam <= 0)
            {
                collider.enabled = false;
                anim.SetBool("StopOnAttacking", false);
                spamming = false;
                //Game Over
                SceneManager.LoadScene(2);
            }

            if (currentTimer >= timerMeteor)
            {
                Destroy(currentMeteor.gameObject);
                anim.SetBool("StopOnAttacking", false);
                spamming = false;
                spawner.asteroidEvent = false;
            }

            if (Input.GetKeyDown(saveHitTouch))
            {
                currentTimerSpam = timerMeteorSpam;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            if (currentMeteor == null)
            {
                currentMeteor = collision.GetComponent<Meteor>();
            }
            if (currentMeteor.asteroid)
            {
                spamming = true;
                currentMeteor.crashing = false;
                anim.SetBool("StopOnAttacking", true);
            }
            else
            {
                score += collision.GetComponent<Meteor>().score;
                Destroy(collision.gameObject);
            }
        }
    }
}