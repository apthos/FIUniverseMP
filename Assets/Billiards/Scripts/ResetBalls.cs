using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBalls : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    Transform spawnPoint;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoint = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        Destroy(GameObject.FindGameObjectWithTag("balls"));
        Instantiate(ballPrefab, spawnPoint);
    }
}
