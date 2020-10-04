using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
