using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelExit : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NextLevelLoad());
        
    }

    IEnumerator NextLevelLoad()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int scenesNumber = SceneManager.sceneCount;
        if (currentSceneIndex != scenesNumber - 2)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(scenesNumber);
        }
    }
}
