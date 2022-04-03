using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public int fieldRadius = 100;
    public int asteroidCount = 50;
    public Vector3 innerSpace;

    public float initialDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayInstantatingAsteroids());
    }
    IEnumerator DelayInstantatingAsteroids(){
        yield return new WaitForSeconds(initialDelay);
        InstantiateAsteroids();
    }

    void InstantiateAsteroids(){
        for(int loop = 0; loop < asteroidCount ; loop++)
        {
            var temp = Instantiate(asteroidPrefabs[Random.Range(0,asteroidPrefabs.Length)], (Random.insideUnitSphere + innerSpace) * fieldRadius, Random.rotation, transform);
            temp.transform.localScale = temp.transform.localScale * Random.Range(0.5f,2.5f);
        }
    }
}
