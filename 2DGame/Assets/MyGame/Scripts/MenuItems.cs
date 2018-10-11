using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItems : MonoBehaviour {

	public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");    
    }

    public void Quit()
    {
        Application.Quit();
    }

}
