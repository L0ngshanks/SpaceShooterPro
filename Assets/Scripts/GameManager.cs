using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool canRestartLevel = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canRestartLevel)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void RestartLevel()
    {
        canRestartLevel = true;
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
