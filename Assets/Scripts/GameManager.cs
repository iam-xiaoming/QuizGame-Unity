using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        SetGameActive(true, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isCompleted)
        {
            SetGameActive(false, true);
            endScreen.ShowFinalScore();
        }
    }

    void SetGameActive(bool quizState, bool endScreenState)
    {
        quiz.gameObject.SetActive(quizState);
        endScreen.gameObject.SetActive(endScreenState);
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
