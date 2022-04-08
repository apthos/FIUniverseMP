using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField] Transform cueBallRespawn;

    [SerializeField] AudioClip myClip;
    AudioSource myAudio;
    // Start is called before the first frame update
    void Awake()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    void MoveCueBall(Collision cueBall)
    {
        Rigidbody rb = cueBall.gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.gameObject.transform.position = cueBallRespawn.position;
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit " + collision.collider.tag);
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "billiardBall":
                Destroy(collision.collider);
                if (!myAudio.isPlaying)
                {
                    myAudio.PlayOneShot(myClip);
                }
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Vector3 leanLocation = new Vector3(this.transform.position.x, this.transform.position.y + .07f, this.transform.position.z);
                LeanTween.move(collision.gameObject, leanLocation, 0.08f).setEase(LeanTweenType.easeOutExpo);
                Destroy(collision.gameObject, 1f);
                break;
            case "cueBall":
                if (!myAudio.isPlaying)
                {
                    myAudio.PlayOneShot(myClip);
                }
                MoveCueBall(collision);
                break;
        }
    }
}
