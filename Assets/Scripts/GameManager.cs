using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// this script is used to manage the whole game core
/// we manage the menu, game over, score and buttons
/// 
/// at the start of the scrit we set the values to false and get the best score
/// 
/// in the Update we manage the score text 
/// and check if the game over is true
/// 
/// the methods are used to manage UI components like menu or gameover
/// or the tutorial panel as well
/// 
/// in the CheckBestScore method we check if the actual score is greater then the best score
/// if its true then the best score will be equal to the score
/// else not
/// anyway show the two scores to the relative UI text 
/// </summary>


public class GameManager : MonoBehaviour
{

    public int score = 0;
    public int best;
    public bool isGameOver, isStarted;

    [SerializeField] Text scoreText, gameOverScoreTxt, bestScoreTxt;
    [SerializeField] Image medalImg;
    [SerializeField] GameObject mainMenuPanel, tutorialPanel, gameOverPanel;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isStarted = false;

        best = PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreHUD();

        if (isGameOver) GameOver();

    }


    public void OpenTutorial()
    {
        mainMenuPanel.GetComponent<Animator>().SetTrigger("close");
        tutorialPanel.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(ActiveSpawner());
        scoreText.gameObject.SetActive(true);

        StartCoroutine(ActivePlayer()) ;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    IEnumerator ActivePlayer()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    IEnumerator ActiveSpawner()
    {
        yield return new WaitForSeconds(3f);
        spawner.SetActive(true);
        StartCoroutine(ActiveSpawner());
    }

    // Example code in GameManager.cs
    private void UpdateScoreHUD()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("scoreText is null in UpdateScoreHUD");
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over method is called");
    
        scoreText.gameObject.SetActive(false);
        CheckBestScore();
        CheckMedal();
        gameOverPanel.SetActive(true);
    }

    void CheckMedal()
    {
        Debug.Log("CheckMedal method is called");
    
        if (score >= 100) 
            medalImg.gameObject.SetActive(true);
    }

    void CheckBestScore()
    {
        Debug.Log("CheckBestScore method is called");
    
        best = PlayerPrefs.GetInt("BestScore");

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", best);
        }

        gameOverScoreTxt.text = score.ToString();
        bestScoreTxt.text = best.ToString();
    }

}
