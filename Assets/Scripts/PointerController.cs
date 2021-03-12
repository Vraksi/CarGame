using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    CheckpointSystem checkpointSystem;
    private int currentCheckpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSystem = FindObjectOfType<CheckpointSystem>();
    }

    private void FixedUpdate()
    {
        gameObject.transform.LookAt(checkpointSystem.checkPoints[currentCheckpoint].transform);
    }

    public void NextCheckpoint()
    {
        currentCheckpoint++;
    }
}
