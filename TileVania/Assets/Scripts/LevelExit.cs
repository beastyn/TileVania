using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelExit : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1f;

    GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        StartCoroutine(NextLevelLoad());
        
    }

    IEnumerator NextLevelLoad()
    {
        gameManager = FindObjectOfType<GameManager>();
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int scenesNumber = SceneManager.sceneCountInBuildSettings;
        gameManager.ResetGameSession(currentSceneIndex + 1);
    }
}
