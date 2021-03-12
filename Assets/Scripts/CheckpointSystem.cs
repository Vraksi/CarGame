using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    List<BoxCollider> checkPoints;

    //For debugging purpose
    [SerializeField] int checkpointsInScene;

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = new List<BoxCollider>();
        foreach (BoxCollider item in GetComponentsInChildren<BoxCollider>())
        {
            checkPoints.Add(item);
            //Debug.Log(item.name);
        }
        checkpointsInScene = checkPoints.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
              
    }

    public void hitCheckPoint()
    {
        checkpointsInScene--;
        if (checkpointsInScene <= 0)
        {
            Debug.Log("You completed the course");
            //TODO DISPLAY TIME OR SCORE
        }
    }
}
