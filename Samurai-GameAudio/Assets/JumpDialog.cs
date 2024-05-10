using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDialog : MonoBehaviour
{
    private FMOD.Studio.EventInstance jumpEventInstance;

    void Start()
    {
        // Create an instance of the FMOD event
        jumpEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/JUMP");
    }

    void Update()
    {
        // Check if the 'C' key is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Start playing the Crouch event
            jumpEventInstance.start();
        }
    }

    private void OnDestroy()
    {
        // release the event instance when it's no longer needed
        jumpEventInstance.release();
    }
}
