using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void StartMenuLoad()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadSpecificLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
