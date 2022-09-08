using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    public AudioSource soundTitle;
    private bool StartingBegin = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && anim.GetCurrentAnimatorStateInfo(0).IsName("PressStart"))
        {
            anim.SetBool("Starting", true);
            StartingBegin = true;
            StartCoroutine(WaitAndPrint(2.0f));
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PressStart"))
        {
            soundTitle.enabled = true;
        }
        //anim.SetBool("StartAudio", true);
        //if(anim.GetBool("StartAudio") == true)
        //{
        //    soundTitle.Play();
        //}
        if (StartingBegin)
        {
            soundTitle.volume -= 0.05f;
        }
    }

    
    
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("Antoine");
        
    }



}
