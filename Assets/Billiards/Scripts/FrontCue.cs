using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FrontCue : MonoBehaviour
{
    Transform attachPointRef;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hand" && Input.GetButton("Axis1D.SecondaryIndexTrigger"))
        {
            UpdateAttachPoint();
        }
    }

    void UpdateAttachPoint()
    {
        Debug.Log("Pressed Left Grip");
    }
}
