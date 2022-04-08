using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardBall : MonoBehaviour
{
    Rigidbody rb;
    float speed;
    [SerializeField] float speedConstraint;
    Vector3 oldPosition;
    bool hit = false;

    [SerializeField] AudioClip myClip;
    AudioSource myAudio;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    void ConstrainRb()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void UnConstrainRb()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hit) speedUpdate();
    }

    void speedUpdate()
    {
        speed = Vector3.Distance(oldPosition, transform.position);
        oldPosition = transform.position;

        if(speed < speedConstraint)
        {
            slowDown();
        }
    }

    void slowDown()
    {
        ConstrainRb();
        UnConstrainRb();
        stopHit();
    }

    void Hit()
    {
        hit = true;
    }

    void stopHit()
    {
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "cueTip" || collision.collider.tag == "billiardBall" || collision.collider.tag == "cueBall")
        {
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(myClip);
            }
            Invoke("Hit", 0.3f);
        }
    }
}
