using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton Declaration
    public static GameManager instance = null;

    //Score keeping variables
    public TMP_Text txtScore;
    private int score = 0;
    const string preText = "Score: ";
    const int ptsPerBounce = 1000;

    //Pause variable
    private float oldTime = 0;

    //Difficulty variable
    const float timeScaleLimit = 3.0f; //Maximum Speed
    const float speedIncrease = 0.03f; //Speed increase

    //Game over variable and buttons variable
    public GameObject smoke;
    private bool gameOver = false;
    public GameObject btnRetry;
    public GameObject btnExit;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        //This is to check that there is only one instance of GameManager running at a time
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Game Over
    public void GameOver(GameObject ball)
    {
        smoke.transform.position = ball.transform.position; //Set the position of the smoke of the ball
        Destroy(ball); // Destroy the ball
        Time.timeScale = 1.0f; //Reset time scale
        smoke.GetComponent<ParticleSystem>().Play(); //Play the particle

        if (!gameOver)
        {
            //Set as gameover
            gameOver = true;
            //Activate buttons
            btnExit.SetActive(true);
            btnRetry.SetActive(true);
            //Woosh SFX
            smoke.GetComponent<AudioSource>().Play();
            //Free and show cursor when game over
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void IncreaseDifficulty()
    {
        if (Time.timeScale >= timeScaleLimit) return;
        Time.timeScale += speedIncrease;
    }



    // Start is called before the first frame update
    void Start()
    {
        //Display the score
        //Disable exit and retry buttons
        DisplayScore();
        btnExit.SetActive(false);
        btnRetry.SetActive(false);
        //Locks cursor in middle of screen when playing
        //Makes cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Quit function
    public void Quit()
    {
        Application.Quit(); //Only works in a build
    }

    // Retry button
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    //Properly format Score Text
    private void DisplayScore()
    {
        //Display 8 digits in score (Score: 00000000)
        txtScore.text = preText + score.ToString("D8");
    }

    //Increase score for every bounce of ball on paddle
    public void AddPoints()
    {
        score += ptsPerBounce; //Adds 1000 points
        DisplayScore(); //Update new score
        IncreaseDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        //Pause
        if (Input.GetButtonUp("Cancel") && !gameOver)
        {
            float temp = oldTime;
            oldTime = Time.timeScale;
            Time.timeScale = temp;

            //When pausing, free the cursor
            //And make it visible
            Cursor.lockState = (temp > 0f) ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = (temp > 0f) ? false : true;
        }
    }
}
