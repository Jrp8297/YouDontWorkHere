using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToInstructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Play()
    {
        SceneManager.LoadScene("Diner");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
