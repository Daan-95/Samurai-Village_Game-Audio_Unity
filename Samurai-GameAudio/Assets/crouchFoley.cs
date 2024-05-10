using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchFoley: MonoBehaviour
{
    private FMOD.Studio.EventInstance crouchEventInstance;

    void Start()
    {
        // Create an instance of the FMOD event
        crouchEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Crouch");
    }

    void Update()
    {
        // Check if the 'C' key is pressed down
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Start playing the Crouch event
            crouchEventInstance.start();
        }
    }

    private void OnDestroy()
    {
        // release the event instance when it's no longer needed
        crouchEventInstance.release();
    }
}
