using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{ 
    [Header("UI Elements")]
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Image livesImage = null;
    [SerializeField]
    private Sprite[] liveSprites = null;
    [SerializeField]
    private Text gameOverText = null;
    [SerializeField]
    private Text restartText = null;
    [Header("GameManager")]
    [SerializeField]
    private GameObject gameManager = null;

    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 00";
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        if(gameManager == null)
        {
            Debug.LogError("GameManager is equal to NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            CycleGameOverTextColor();
        }

    }

    public void UpdatePlayerScore(int currentScore)
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        if(livesImage != null)
        {
            livesImage.sprite = liveSprites[currentLives];
        }
    }

    public void UpdateGameOverText(bool isAlive)
    {
        if(!isAlive)
        {
            if(gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
            }
            if (restartText != null)
            {
                restartText.gameObject.SetActive(true);
            }

            if(gameManager != null)
            {
                gameManager.GetComponent<GameManager>().RestartLevel();
            }

            isGameOver = true;
        }
    }

    void CycleGameOverTextColor()
    {
        int colorID = UnityEngine.Random.Range(0, 3);
        switch (colorID)
        {
            case 0:
                gameOverText.color = Color.red;
                break;
            case 1:
                gameOverText.color = Color.green;
                break;
            case 2:
                gameOverText.color = Color.blue;
                break;
            default:
                gameOverText.color = Color.red;
                break;
        }
    }
}
