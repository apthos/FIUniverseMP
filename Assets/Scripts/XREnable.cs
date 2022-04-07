using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XREnable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsXREnabled())
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
            mainCamera.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsXREnabled()
    {
        var xrInputSubsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(xrInputSubsystems);

        foreach (var subsystem in xrInputSubsystems)
        {
            if (subsystem.running)
            {
                return true;
            }
        }

        return false;
    }
}
