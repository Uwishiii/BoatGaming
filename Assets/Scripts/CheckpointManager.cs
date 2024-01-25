using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private Checkpoint[] checkpoints;

    [SerializeField]
    private GameObject compassArrow;


    // Update is called once per frame
    void FixedUpdate()
    {
        SmoothRotate(GetAngle());
    }


    // Look at the checkpoints array and return the first checkpoint that has not been reached
    private Checkpoint GetFirstUnreachedCheckpoint()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (!checkpoint.CheckpointReached)
            {
                return checkpoint;
            }
        }
        return null;
    }

    // rotate compass arrow to point at the first unreached checkpoint
    private Quaternion GetAngle()
    {
        Checkpoint checkpoint = GetFirstUnreachedCheckpoint();
        if (checkpoint == null)
        {
            //Debug.Log("No Checkpoint Found");
            return Quaternion.identity;
        }

       // Funny math stuff to get the angle between the player and the checkpoint and rotate the compass arrow
        Vector3 relativeCheckpointLocation = transform.InverseTransformPoint(checkpoint.transform.position);
        float angle = Mathf.Atan2(relativeCheckpointLocation.z, relativeCheckpointLocation.x) * Mathf.Rad2Deg;
        //compassArrow.transform.localRotation = Quaternion.Euler(0, 0, -angle);
        return Quaternion.Euler(0, 0, -angle);
    }

    private void SmoothRotate(Quaternion angle)
    {
        //Debug.Log(angle);
        compassArrow.transform.localRotation = Quaternion.Slerp(compassArrow.transform.localRotation, angle, Time.deltaTime * 2);


    }


}
