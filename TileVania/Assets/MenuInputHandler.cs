using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputHandler : MonoBehaviour {
    
    void Start () {

        MouseVisibility();

    }

    private void MouseVisibility()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      /*   if (currentSceneIndex == 0 || currentSceneIndex == 3 || currentSceneIndex == 4)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        Debug.Log(Cursor.visible);*/

    }


    // Update is called once per frame
    void Update () {

        QuitGame();

    }

    void QuitGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
