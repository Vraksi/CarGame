using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    GameObject inGameMenu;

    private void Start()
    {
        inGameMenu = GameObject.Find("InGameMenu");
        inGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            inGameMenu.SetActive(true);
        }
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
        inGameMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
