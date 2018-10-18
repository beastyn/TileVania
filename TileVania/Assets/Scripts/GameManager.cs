using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] int playerLives = 3;

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

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives = playerLives-1;
        Debug.Log("Called Take Life");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
       
    }
    
        
    
}
