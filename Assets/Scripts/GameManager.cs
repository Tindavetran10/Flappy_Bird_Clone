using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;
    public int best;
    public bool isGameOver, isStarted;
    private bool _hasDied;

    [SerializeField] private Text scoreText, gameOverScoreTxt, bestScoreTxt;
    [SerializeField] private Image medalImg;
    [SerializeField] private GameObject mainMenuPanel, tutorialPanel, gameOverPanel;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Player player;
    
    private static readonly int Close = Animator.StringToHash("close");


    // Start is called before the first frame update
    private void Start()
    {
        isGameOver = false;
        isStarted = false;

        best = PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScoreHUD();
        
        if (!_hasDied && spawner.GetComponent<Spawner>().CheckWallCollision(player.GetBounds()))
        {
            Debug.Log("Collision detected with the player.");
            player.dieAudio.Play();
            _hasDied = true;
            GameOver();
        }
        
        if (!_hasDied && spawner.GetComponent<Spawner>().GapPassed(player.GetBounds()))
        {
            score++;
            player.pointAudio.Play();
            Debug.Log("Score increased: " + score);
        }
        
        if (isGameOver) GameOver();
    }
    
    public void OpenTutorial()
    {
        mainMenuPanel.GetComponent<Animator>().SetTrigger(Close);
        tutorialPanel.SetActive(true);
    }
    
    public void StartGame()
    {
        isStarted = true;
        StartCoroutine(ActiveSpawner());
        scoreText.gameObject.SetActive(true);

        StartCoroutine(ActivePlayer());
        player.GetComponent<AIController>().EnableAI(); // Enable AI Controller
    }

    public void RestartGame() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    private IEnumerator ActivePlayer()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<Player>().enabled = true;
    }

    private IEnumerator ActiveSpawner()
    {
        yield return new WaitForSeconds(3f);
        spawner.SetActive(true);
        StartCoroutine(ActiveSpawner());
    }

    // Example code in GameManager.cs
    private void UpdateScoreHUD()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        else Debug.LogError("scoreText is null in UpdateScoreHUD");
    }

    private void GameOver()
    {
        Debug.Log("Game Over method is called");
    
        scoreText.gameObject.SetActive(false);
        CheckBestScore();
        CheckMedal();
        gameOverPanel.SetActive(true);
        player.GetComponent<AIController>().DisableAI(); // Disable AI Controller
    }

    private void CheckMedal()
    {
        Debug.Log("CheckMedal method is called");
    
        if (score >= 100) 
            medalImg.gameObject.SetActive(true);
    }

    private void CheckBestScore()
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
