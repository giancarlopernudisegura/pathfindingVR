using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    public void buttonStart() {

        SceneManager.LoadScene(1);
        Debug.Log("Play button pressed");
    
    }

    public void buttonQuit() {

        Application.Quit();
        Debug.Log("Quit button pressed");

    }

}