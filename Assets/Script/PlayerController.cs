using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public Spawner spawner;

    Meteor currentMeteor;
    public GameObject pauseMenu;

    public AudioClip batte,explosion,moyenne,petite,contact;
    AudioSource audio;
    public Animator animCanvas;
    bool end,pause;
    public TextMeshProUGUI scoreText;

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
        audio = GetComponent<AudioSource>();
        scoreText.text = "SCORE: "+score;
    }

    void Update()
    {
        if (!end)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 0)
                {
                    pauseMenu.SetActive(false);
                    pause = false;
                    Time.timeScale = 1;
                }
                else
                {
                    pauseMenu.SetActive(true);
                    pause = true;
                    Time.timeScale = 0;
                }
            }
            if (!pause)
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
                        collider.offset = new Vector2(-0.2f, 0.2f);
                        sprite.flipX = false;
                        anim.SetTrigger("Attacking");
                        attacking = true;
                        saveHitTouch = hitLeft;
                        audio.PlayOneShot(batte);
                    }
                    else if (Input.GetKeyDown(hitRight))
                    {
                        collider.offset = new Vector2(0.2f, 0.2f);
                        sprite.flipX = true;
                        anim.SetTrigger("Attacking");
                        attacking = true;
                        saveHitTouch = hitRight;
                        audio.PlayOneShot(batte);
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
                        ScoreSave.SaveScore(score);
                        SceneManager.LoadScene(2);
                    }

                    if (currentTimer >= timerMeteor)
                    {
                        anim.SetBool("StopOnAttacking", false);
                        Destroy(currentMeteor.gameObject);
                        spawner.asteroidEvent = false;
                        spamming = false;
                        audio.PlayOneShot(explosion);
                    }

                    if (Input.GetKeyDown(saveHitTouch))
                    {
                        currentTimerSpam = timerMeteorSpam;
                        audio.PlayOneShot(batte);
                    }
                }
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
                audio.PlayOneShot(contact);
            }
            else
            {
                score += collision.GetComponent<Meteor>().score;
                scoreText.text = "SCORE:" + score;
                if (collision.gameObject.GetComponent<Meteor>().littleMeteor)
                    audio.PlayOneShot(petite);
                else
                    audio.PlayOneShot(moyenne);

                Destroy(collision.gameObject);
            }
        }
    }

    public IEnumerator End()
    {
        animCanvas.SetTrigger("StartFadeOut");
        end = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}