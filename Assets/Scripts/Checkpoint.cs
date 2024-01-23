using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool checkpointReached = false;

    public bool CheckpointReached
    {
        get { return checkpointReached; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !checkpointReached)
        {
            checkpointReached = true;
            Debug.Log("Checkpoint reached!");
        }
    }

}
