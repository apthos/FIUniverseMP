using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] AudioClip bowlingSound;
    [SerializeField] GameObject Lane;
    AudioSource myAudio;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    void DestroyPin()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((!myAudio.isPlaying) && collision.collider.tag != "lane")
        {
            myAudio.PlayOneShot(bowlingSound);
        }
        if(collision.collider.tag == "ball")
        {
            rb.constraints = RigidbodyConstraints.None;
            Invoke("DestroyPin", 2f);
        }
    }
}
