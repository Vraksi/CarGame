using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointSystem : MonoBehaviour
{
    public List<BoxCollider> checkPoints;
    [SerializeField] float pointsPerCheckpoint = 50f;
    [SerializeField] float speedPointMultiplier = 4f;
    private float score = 0;
    GameObject UIpointScore;
    PointerController pointerController;

    //For debugging purpose
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
            
            Debug.Log("You completed the course");
            //TODO DISPLAY TIME OR SCORE
        }
    }

    public void AddPoints(float speed)
    {
        pointerController.NextCheckpoint();
        score += pointsPerCheckpoint * (speed/speedPointMultiplier);
        //Debug.Log(Mathf.Abs(score));
        UIpointScore.GetComponent<TextMeshProUGUI>().SetText(Mathf.RoundToInt(score).ToString());
    }
}
