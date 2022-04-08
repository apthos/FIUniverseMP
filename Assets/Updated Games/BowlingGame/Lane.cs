using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] Transform[] pinLocArr;
    [SerializeField] GameObject pinPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform PinSpawnLocation;
    [SerializeField] GameObject PinsPrefab;
    GameObject ball;

    int turnCounter = 0;

    private void Reset()
    {
        turnCounter = 0;
        SpawnPin();
    }

    void SpawnPin()
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pins");
        foreach(GameObject x in pins)
        {
            Destroy(x);
        }
        SpawnPins();
    }

    public void SpawnPins()
    {
        Instantiate(PinsPrefab, PinSpawnLocation.position, PinSpawnLocation.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        ball = other.gameObject;
            if (other.tag == "ball" && turnCounter == 0)
            {
                turnCounter++;
            }
            else if (other.tag == "ball" && turnCounter == 1)
            {
                turnCounter++;
                Invoke("Reset", 2f);
            }
    }
}
