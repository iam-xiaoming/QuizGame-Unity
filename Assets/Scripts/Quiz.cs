using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = false;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Timer timer;
    [SerializeField] Image timerImage;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    int maximumQuestion;
    int questionCount = 0;
    public bool isCompleted = false;

    void Start()
    {
        maximumQuestion = questions.Count;    
    }


    void Update()
    {
        // We show the circle progress here.
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (questionCount == maximumQuestion)
            {
                isCompleted = true;
                return;
            }

            hasAnsweredEarly = false;
            timer.loadNextQuestion = false;
            GetNextQuestion();
            UpdateProgressBar();
        }
        // If time is run out and users haven't clicked on answers yet,
        // then we display the correct answer and change sprite of the correct button;
        // otherwise, see "OnAnswerSelected" method.

        // SetButtonState means we don't want anyone to interact with those buttons when it runs out of time.
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            // When users don't choose any answers, we pass index as -1,
            // which means, the index never matches the correct index.
            DisplayAnswers(-1);
            SetButtonState(false);
        }
    }

    void UpdateProgressBar()
    {
        progressBar.value = (float)questionCount / maximumQuestion;
    }

    void DisPlayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI answerText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerText.text = currentQuestion.GetAnswer(i);
        }
    }

    // If users click on answer button, we show correct answer and then set button'sprite as default.
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswers(index);
        SetButtonState(false);

        // This method means to set everything as default in Timer.cs script.
        timer.CancelTimer();

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswers(int index)
    {
        int correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            scoreKeeper.CorrectAnswers++;
        }
        else
        {
            questionText.text = $"Sorry, the correct answer is \"{currentQuestion.GetAnswer(correctAnswerIndex)}\".";
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            GetRandomQuestion();
            SetButtonState(true);
            SetDefaultButtonSprites();
            DisPlayQuestion();
            scoreKeeper.QuestionSeen++;
            questionCount++;
        }
    }

    void GetRandomQuestion()
    {
        // Debug.Log(questions.Count);

        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void SetDefaultButtonSprites()
    {
        foreach (GameObject answerButton in answerButtons)
        {
            Image buttonImage = answerButton.GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        foreach (GameObject answerButton in answerButtons)
        {
            Button button = answerButton.GetComponent<Button>();
            button.interactable = state;
        }
    }
}
