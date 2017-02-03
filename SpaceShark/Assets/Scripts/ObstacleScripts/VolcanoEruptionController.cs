using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEruptionController : MonoBehaviour
{
    // The animator attached to the game object
    private Animator animator = null;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float playerDistance = gameObject.transform.position.z - Ship_Movement.shipPosition.z;

       // Debug.Log("player dist: " + playerDistance);
       // Debug.Log("range: " + GameSettings.eruptionRange);

        if (playerDistance < GameSettings.eruptionRange)
        {
         //   Debug.Log("range");
            animator.SetBool("ReadyToErupt", true);
        }
        else
        {
            animator.SetBool("ReadyToErupt", false);
        }
    }
}
