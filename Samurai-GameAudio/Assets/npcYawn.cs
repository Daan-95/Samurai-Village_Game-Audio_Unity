using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcYawn : MonoBehaviour
{
    // This will hold the FMOD event path
    [SerializeField]
    private FMOD.Studio.EventInstance yawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Create the FMOD event instance
        yawnEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Yawn");
        yawnEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    // This method should be public to be accessible by the animation event
    public void PlayYawn()
    {
        // Set 3D attributes each time to ensure they are updated to the current object position
        yawnEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        // Start the event
        yawnEvent.start();

        // It's a good practice to release the event instance after playing if it's a one-shot sound
        yawnEvent.release();

        // Recreate the instance for future use since it's released after playing
        yawnEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Yawn");
        yawnEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    void OnDestroy()
    {
        // Properly release the event instance when the object is destroyed
        yawnEvent.release();
    }
}