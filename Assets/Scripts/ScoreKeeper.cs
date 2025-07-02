using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int correctAnswers = 0;
    int questionSeen = 0;

    public int CorrectAnswers
    {
        get { return correctAnswers; }
        set { correctAnswers = value; }
    }

    public int QuestionSeen
    {
        get { return questionSeen; }
        set { questionSeen = value; }
    }

    public int CalculateScore()
    {
        float percentage = (float)correctAnswers / questionSeen * 100;
        return (int)percentage;
    }
}
