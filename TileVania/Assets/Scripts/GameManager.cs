using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] float resetDelay = 2f;

    [SerializeField] Text liveText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        int numGameManagers = FindObjectsOfType<GameManager>().Length ;

        if (numGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        liveText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}
	

    public void ProcessPlayerDeath()
    {
        Debug.Log("Called Process");
        
        if (playerLives > 1)
        {
            TakeLife(); 
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointToAdd)
    {
        score += pointToAdd;
        scoreText.text = score.ToString();
    }

    private void ResetGameSession()
    {
        StartCoroutine(LoadingLevel(0));
        
    }

    private void TakeLife()
    {
        playerLives = playerLives-1;
        liveText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadingLevel(currentSceneIndex));
       
    }

    IEnumerator LoadingLevel(int sceneIndex)
    {
        yield return new WaitForSeconds(resetDelay);
        SceneManager.LoadScene(sceneIndex);
        if (sceneIndex == 0)
        {
            Destroy(gameObject);
        }
        
    }
        
    
}
