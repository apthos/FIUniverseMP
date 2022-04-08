using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    Collider ball;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ball")
        {
            ball = other;
            Invoke("moveBall", 3f);
        }
    }

    void moveBall()
    {
        ball.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ball.gameObject.transform.position = spawnPoint.position;
        ball.gameObject.transform.rotation = spawnPoint.rotation;
        ball.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
