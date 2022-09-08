using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loader : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI currentScore;
    public GameObject newToActivate;

    void Start()
    {
        ScoreData list = ScoreSave.GetScores();
        int save=0;
        if(list == null)
        {
            Debug.LogError("No list");
        }
        foreach(int i in list.listScore)
        {
            if (i > save) 
                save = i;
        }

        if(list.listScore.IndexOf(save)==0)
        {
            newToActivate.SetActive(true);
            highScore.text =  save.ToString();
            currentScore.text = highScore.text;
        }
        else
        {
            highScore.text = save.ToString();
            currentScore.text = list.listScore[0].ToString();
        }
    }
}
