using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public Animator anim;
    bool choice;
    private void Update()
    {
        if (!choice && anim.GetCurrentAnimatorStateInfo(0).IsName("EndButton")) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                choice = true;
                StartCoroutine(retry());
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private IEnumerator retry()
    {
        anim.SetTrigger("StartFadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
