using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointSystem : MonoBehaviour
{
    public List<BoxCollider> checkPoints;
    [SerializeField] float pointsPerCheckpoint = 50f;
    [SerializeField] float speedPointMultiplier = 4f;
    
    [SerializeField] GameObject submitScoreUI;
    private float score = 0;
    GameObject UIpointScore;
    PointerController pointerController;

    [Header("For debugging")]
    [SerializeField] int checkpointsInScene;

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = new List<BoxCollider>();
        foreach (BoxCollider item in GetComponentsInChildren<BoxCollider>())
        {
            checkPoints.Add(item);
        }
        checkpointsInScene = checkPoints.Count;
        pointerController = FindObjectOfType<PointerController>();
        UIpointScore = GameObject.Find("PointsAcquired");
        UIpointScore.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }

    public void hitCheckPoint(float speed)
    {
        checkpointsInScene--;
        AddPoints(speed);
        if (checkpointsInScene <= 0)
        {
            Time.timeScale = 0;
            submitScoreUI.SetActive(true);
            //############## IMPORTANT #################### TheScore needs to be top in heirachy for this to work. Should maybe have a rework.
            submitScoreUI.GetComponentInChildren<TextMeshProUGUI>().SetText(Mathf.RoundToInt(score).ToString());
            Debug.Log("You completed the course");            
        }
    }



    public void AddPoints(float speed)
    {
        pointerController.NextCheckpoint();
        score += pointsPerCheckpoint * (speed/speedPointMultiplier);
        //Debug.Log(Mathf.Abs(score));
        UIpointScore.GetComponent<TextMeshProUGUI>().SetText(Mathf.RoundToInt(score).ToString());
    }

    public void SubmitHighscore()
    {
        //TODO Needs Connection to server
        Time.timeScale = 1;
        submitScoreUI.SetActive(false);
    }
}
