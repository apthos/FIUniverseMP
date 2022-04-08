using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinGrabber : MonoBehaviour
{
    [SerializeField] Transform PinSpawnLocation;
    [SerializeField] GameObject PinsPrefab;
    public bool resetting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pin")
        {
            Reset(other.gameObject);
        }
    }

    public void changeBool()
    {
        resetting = true;
    }
    public void Reset(GameObject pin)
    {
        if (resetting)
        {
            Debug.Log("resetting");
            Destroy(pin);
        }
    }

    public void SpawnPins()
    {
        Instantiate(PinsPrefab, PinSpawnLocation.position, PinSpawnLocation.rotation);
    }
}
