using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public float tweenTime;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1f,1f,1f),tweenTime).setEase(LeanTweenType.easeOutSine);
    }

    public void Leave()
    {
        LeanTween.scale(gameObject, new Vector3(0f,0f,0f), tweenTime).setEase(LeanTweenType.easeOutSine).setOnComplete(EliminatePlanet);
    }

    public void EliminatePlanet(){
        Destroy(gameObject);
    }
}
