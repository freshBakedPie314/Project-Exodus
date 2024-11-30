using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    public bool t = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (t)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                LoadMenu();
            }
        }
    }

    public void LoadGame()
    {
        Debug.Log("Game");
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Debug.Log("Game");
        Application.Quit();
    }
}
