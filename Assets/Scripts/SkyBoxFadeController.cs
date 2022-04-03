using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkyBoxFadeController : MonoBehaviour
{

    public GameObject cameraObject;
    private Skybox skybox;
    private Material mat;

    [SerializeField]
    private float transitionSpeed;
    [SerializeField]
    private bool finishedTransition;

    private Action onFinishingTransition;

    // Start is called before the first frame update
    void Start()
    {
        skybox = cameraObject.GetComponent<Skybox>();
        mat = skybox.material;
        mat.SetFloat("_Blend", 0f);
        mat.SetFloat("_BlendMode", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!finishedTransition){
            var temp  = mat.GetFloat("_Blend");
            if(temp >= 1.0f){
                mat.SetFloat("_Blend", 1f);
                finishedTransition = true;
                if(onFinishingTransition != null){
                    onFinishingTransition();
                    onFinishingTransition = null;
                }    
            }else{
                mat.SetFloat("_Blend", temp + transitionSpeed * Time.deltaTime);
            
            }
            
        }
    }

    public void BeginTransition(DestinationSO presentDestination, DestinationSO toDestination, Action _onFinishedTransition){
        
        onFinishingTransition = _onFinishedTransition;
        //assuming im at 1 already.
        //Make this to 0, then make 1. then start the transition for update to take care of..
        mat.SetTexture("_FrontTex_1", presentDestination.transitionSkyboxTextures[0]);
        mat.SetTexture("_BackTex_1", presentDestination.transitionSkyboxTextures[1]);
        mat.SetTexture("_LeftTex_1", presentDestination.transitionSkyboxTextures[2]);
        mat.SetTexture("_RightTex_1", presentDestination.transitionSkyboxTextures[3]);
        mat.SetTexture("_UpTex_1", presentDestination.transitionSkyboxTextures[4]);
        mat.SetTexture("_DownTex_1", presentDestination.transitionSkyboxTextures[5]);
        mat.SetFloat("_Blend", 0f);

        mat.SetTexture("_FrontTex_2", toDestination.transitionSkyboxTextures[0]);
        mat.SetTexture("_BackTex_2", toDestination.transitionSkyboxTextures[1]);
        mat.SetTexture("_LeftTex_2", toDestination.transitionSkyboxTextures[2]);
        mat.SetTexture("_RightTex_2", toDestination.transitionSkyboxTextures[3]);
        mat.SetTexture("_UpTex_2", toDestination.transitionSkyboxTextures[4]);
        mat.SetTexture("_DownTex_2", toDestination.transitionSkyboxTextures[5]);
        finishedTransition = false;

        //make VFX show up

    }

    public void InitialTransition(DestinationSO presentDestination){

        mat.SetTexture("_FrontTex_1", presentDestination.transitionSkyboxTextures[0]);
        mat.SetTexture("_BackTex_1", presentDestination.transitionSkyboxTextures[1]);
        mat.SetTexture("_LeftTex_1", presentDestination.transitionSkyboxTextures[2]);
        mat.SetTexture("_RightTex_1", presentDestination.transitionSkyboxTextures[3]);
        mat.SetTexture("_UpTex_1", presentDestination.transitionSkyboxTextures[4]);
        mat.SetTexture("_DownTex_1", presentDestination.transitionSkyboxTextures[5]);
        mat.SetFloat("_Blend", 0f);

        mat.SetTexture("_FrontTex_2", presentDestination.transitionSkyboxTextures[0]);
        mat.SetTexture("_BackTex_2", presentDestination.transitionSkyboxTextures[1]);
        mat.SetTexture("_LeftTex_2", presentDestination.transitionSkyboxTextures[2]);
        mat.SetTexture("_RightTex_2", presentDestination.transitionSkyboxTextures[3]);
        mat.SetTexture("_UpTex_2", presentDestination.transitionSkyboxTextures[4]);
        mat.SetTexture("_DownTex_2", presentDestination.transitionSkyboxTextures[5]);
        mat.SetFloat("_Blend", 0f);
        finishedTransition = false;
    }
}
