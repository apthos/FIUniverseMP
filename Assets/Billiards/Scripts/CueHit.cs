using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueHit : MonoBehaviour
{
    Rigidbody cue;
    Vector3 cueVector;
    [SerializeField] float forceMult;
    Vector3 ballForce;

    [SerializeField] AudioClip myClip;
    AudioSource myAudio;

    private void Awake()
    {
        cue = gameObject.GetComponent<Rigidbody>();
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cueVector = cue.transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "billiardBall")
        {
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(myClip);
            }
            HitBall(other);
        } else if(other.tag == "cueBall")
        {
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(myClip);
            }
            HitBall(other);
        }
    }

    void HitBall(Collider ball)
    {
        AddForceOnBall(ball.gameObject.GetComponent<Rigidbody>());
    }

    void AddForceOnBall(Rigidbody ball)
    {
        ballForce = cueVector * forceMult;
        ball.AddForce(ballForce);
    }
}
