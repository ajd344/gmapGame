using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonManager : MonoBehaviour {

	public void NewGameButton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
