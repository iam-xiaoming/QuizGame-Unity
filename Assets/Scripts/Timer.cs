using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public float fillFraction = 1f;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion = false;
    float timeValue = 0;


    void Update()
    {
        UpdateTimer();
    }
    
    // This method to set default, which means "timeValue = 0", we begin with "timeToCompleteQuestion".
    public void CancelTimer()
    {
        timeValue = 0;
    }


    // This method to switch between "timeToCompleteQuestion" and "timeToShowCorrectAnswer".
    // We start with "timeToCompleteQuestion", and right after "timeValue" <= 0, we switch to "timeToShowCorrectAnswer" and so on.
    void UpdateTimer()
    {
        timeValue -= Time.deltaTime;
        if (timeValue <= 0)
        {
            if (!isAnsweringQuestion)
            {
                isAnsweringQuestion = true;
                timeValue = timeToCompleteQuestion; 

                // If "isAnsweringQuestion = false", it's time to go to another question, we set it back to false in Quiz.cs script.
                loadNextQuestion = true;
            }
            else
            {
                isAnsweringQuestion = false;
                timeValue = timeToShowCorrectAnswer;
            }
        }

        // Calculate the time progress.
        if (isAnsweringQuestion)
        {
            fillFraction = timeValue / timeToCompleteQuestion;
        }
        else {
            fillFraction = timeValue / timeToShowCorrectAnswer;
        }
    }
}
